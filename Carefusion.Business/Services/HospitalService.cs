using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Core;
using Carefusion.Core.Criterias;
using Carefusion.Core.Utilities;
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

        public async Task<(IEnumerable<HospitalDto> Hospitals, int TotalCount)> GetAllHospitalsAsync(int pageNumber, int pageSize)
        {
            var (hospitals, totalCount) = await _hospitalRepository.GetAllAsync(pageNumber, pageSize);
            return (_mapper.Map<IEnumerable<HospitalDto>>(hospitals), totalCount);
        }

        public async Task<(IEnumerable<HospitalDto> Hospitals, int TotalCount)> SearchHospitalsAsync(string searchTerm, HospitalFilterCriteria? filterCriteria, HospitalSortCriteria? sortCriteria, int pageNumber, int pageSize)
        {
            var query = _hospitalRepository.SearchHospitals(searchTerm, filterCriteria, sortCriteria);
            var totalCount = await query.CountAsync();

            var hospitals = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var hospitalDtos = _mapper.Map<IEnumerable<HospitalDto>>(hospitals);
            return (hospitalDtos, totalCount);
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
                throw new Authorization.NotFoundException("Hospital not found.");
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