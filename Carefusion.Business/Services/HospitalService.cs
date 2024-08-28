using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Core;
using Carefusion.Data.Interfaces;
using Carefusion.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Business.Services
{
    public class HospitalService : IHospitalService
    {
        private readonly IHospitalRepository _hospitalRepository;
        private readonly IMapper _mapper;
        private readonly TimeZoneInfo _timeZone;

        public HospitalService(IHospitalRepository hospitalRepository, IMapper mapper)
        {
            _hospitalRepository = hospitalRepository;
            _mapper = mapper;
            _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
        }

        public async Task<HospitalDto> GetHospitalByIdAsync(int id)
        {
            var hospital = await _hospitalRepository.GetByIdAsync(id);
            return _mapper.Map<HospitalDto>(hospital);
        }

        public async Task<(IEnumerable<HospitalDto> Hospitals, int TotalCount)> SearchHospitalsAsync(
            string? q,
            string[]? typeField,
            string[]? sortField,
            int pageNumber,
            int pageSize,
            bool showInactive)
        {
            var query = _hospitalRepository.GetQuery();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(h => h.Affiliation != null && (h.Name.Contains(q) || h.Code.Contains(q) || h.Affiliation.Contains(q)));
            }

            if (!showInactive)
            {
                query = query.Where(h => h.Active);
            }

            if (typeField is { Length: > 0 })
            {
                query = query.Where(h => typeField.Contains(h.Type));
            }

            if (sortField is { Length: > 0 })
            {
                query = ApplySorting(query, sortField);
            }

            var totalCount = await query.CountAsync();

            var hospitals = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var hospitalDtos = _mapper.Map<IEnumerable<HospitalDto>>(hospitals);
            return (hospitalDtos, totalCount);
        }

        private static IQueryable<Hospital> ApplySorting(IQueryable<Hospital> query, string[] sortFields)
        {
            return sortFields.Aggregate(query, (current, sortField) => sortField switch
            {
                "numberOfBedsAsc" => current.OrderBy(h => h.NumberOfBeds),
                "numberOfBedsDesc" => current.OrderByDescending(h => h.NumberOfBeds),
                "emergencyServices" => current.OrderByDescending(h => h.EmergencyServices),
                "city" => current.OrderBy(h => h.City),
                "district" => current.OrderBy(h => h.District),
                _ => current
            });
        }

        public async Task AddHospitalAsync(HospitalDto hospitalDto)
        {
            var hospital = _mapper.Map<Hospital>(hospitalDto);
            hospital.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
            await _hospitalRepository.AddAsync(hospital);
        }

        public async Task UpdateHospitalAsync(int id, HospitalDto hospitalDto)
        {
            var hospital = await _hospitalRepository.GetByIdAsync(id);
            if (hospital == null)
            {
                throw new InvalidOperationException();
            }

            _mapper.Map(hospitalDto, hospital);
            hospital.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
            await _hospitalRepository.UpdateAsync(hospital);
        }

        public async Task<bool> DeleteHospitalAsync(int id)
        {
            var hospital = await _hospitalRepository.GetByIdAsync(id);

            await _hospitalRepository.DeleteAsync(hospital);
            return true;
        }
    }
}