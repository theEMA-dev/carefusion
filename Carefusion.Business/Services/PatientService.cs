using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Entities;
using Carefusion.Core;
using Carefusion.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Carefusion.Core.Utilities;

namespace Carefusion.Business.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IMapper _mapper;
        private readonly TimeZoneInfo _timeZone;

        public PatientService(IPatientRepository patientRepository, IMapper mapper)
        {
            _patientRepository = patientRepository;
            _mapper = mapper;
            _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<PatientDto> GetPatientByGovId(string govId)
        {
            var patient = await _patientRepository.GetByGovIdAsync(govId);
            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<(IEnumerable<PatientDto> Patients, int TotalCount)> SearchPatientsAsync(
            string? q,
            PatientSort? sortField,
            Gender? gender,
            BloodType? bloodType,
            DateOnly? birthStartDate,
            DateOnly? birthEndDate,
            int pageNumber,
            int pageSize,
            bool showDeceased,
            bool showInactive)
        {
            var query = _patientRepository.GetQuery();

            if (!string.IsNullOrEmpty(q))
            {
                query = query.Where(p => p.Name.Contains(q));
            }

            if (gender != null)
            {
                query = query.Where(p => p.Gender == gender.ToString());
            }

            if (bloodType != null)
            {
                var bloodTypeValue = bloodType.ReadDesc();
                query = query.Where(p => p.BloodType == bloodTypeValue);
            }

            if (birthStartDate != null && birthEndDate == null)
            {
                query = query.Where(p => p.BirthDate >= birthStartDate);
            }

            if (birthStartDate != null && birthEndDate != null)
            {
                query = query.Where(p => p.BirthDate >= birthStartDate && p.BirthDate <= birthEndDate);
            }

            if (birthStartDate == null && birthEndDate != null)
            {
                query = query.Where(p => p.BirthDate <= birthEndDate);
            }

            if (!showDeceased)
            {
                query = query.Where(p => !p.Deceased);
            }

            if (!showInactive)
            {
                query = query.Where(p => p.Active);
            }

            query = sortField is not null ? ApplySorting(query, sortField) : query.OrderBy(p => p.Identifier);
            var totalCount = await query.CountAsync();

            var patients = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var patientDtos = _mapper.Map<IEnumerable<PatientDto>>(patients);
            return (patientDtos, totalCount);
        }


        private static IQueryable<Patient> ApplySorting(IQueryable<Patient> query, PatientSort? sortField)
        {
            return sortField switch
            {
                PatientSort.alphabeticalAsc => query.OrderBy(p => p.Name),
                PatientSort.alphabeticalDesc => query.OrderByDescending(p => p.Name),
                PatientSort.newest => query.OrderByDescending(p => p.RecordUpdated),
                PatientSort.oldest => query.OrderBy(p => p.RecordUpdated),
                _ => throw new ArgumentOutOfRangeException(nameof(sortField), sortField, null)
            };
        }

        public async Task AddPatientAsync(PatientDto patientDto)
        {
            var patient = _mapper.Map<Patient>(patientDto);
            patient.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
            await _patientRepository.AddAsync(patient);
        }

        public async Task UpdatePatientAsync(int id, PatientDto patientDto)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
            {
                throw new InvalidOperationException();
            }

            _mapper.Map(patientDto, patient);
            patient.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
            await _patientRepository.UpdateAsync(patient);
        }

        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            await _patientRepository.DeleteAsync(patient);
            return true;
        }
    }
}
