using System.Globalization;
using Carefusion.Business.Interfaces;
using Carefusion.Entities;
using Carefusion.Core;
using Carefusion.Data.Interfaces;

namespace Carefusion.Business.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientDto> GetPatientByIdAsync(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            return new PatientDto
            {
                PatientId = patient.PatientId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                BirthDate = patient.BirthDate,
                Gender = patient.Gender,
                Email = patient.Email,
                Telephone = patient.Telephone,
                Height = patient.Height,
                Weight = patient.Weight,
                BloodType = patient.BloodType,
                Province = patient.Province,
                Picture = patient.Picture
            };
        }

        public async Task AddPatientAsync(PatientDto patientDto)
        {
            var patient = new Patient
            {
                FirstName = patientDto.FirstName,
                LastName = patientDto.LastName,
                BirthDate = patientDto.BirthDate,
                Gender = patientDto.Gender ?? throw new InvalidOperationException(),
                Email = patientDto.Email,
                Telephone = patientDto.Telephone,
                Height = patientDto.Height,
                Weight = patientDto.Weight,
                BloodType = patientDto.BloodType,
                Province = patientDto.Province ?? throw new InvalidOperationException(),
                Picture = patientDto.Picture
            };
            await _patientRepository.AddAsync(patient);
        }

        public async Task UpdatePatientAsync(int id, PatientDto patientDto)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            if (patient == null)
            {
                throw new Utilities.NotFoundException("Patient not found.");
            }

            if (!string.IsNullOrEmpty(patientDto.FirstName))
            {
                patient.FirstName = patientDto.FirstName;
            }

            if (!string.IsNullOrEmpty(patientDto.LastName))
            {
                patient.LastName = patientDto.LastName;
            }

            if (!string.IsNullOrEmpty(patientDto.BirthDate.ToString(CultureInfo.CurrentCulture)))
            {
                patient.BirthDate = patientDto.BirthDate;
            }

            if (patientDto.Gender != null)
            {
                patient.Gender = patientDto.Gender;
            }

            if (!string.IsNullOrEmpty(patientDto.Email))
            {
                patient.Email = patientDto.Email;
            }

            if (!string.IsNullOrEmpty(patientDto.Telephone))
            {
                patient.Telephone = patientDto.Telephone;
            }

            if (patientDto.Height.HasValue)
            {
                patient.Height = patientDto.Height.Value;
            }

            if (patientDto.Weight.HasValue)
            {
                patient.Weight = patientDto.Weight.Value;
            }

            if (!string.IsNullOrEmpty(patientDto.BloodType))
            {
                patient.BloodType = patientDto.BloodType;
            }

            if (!string.IsNullOrEmpty(patientDto.Province))
            {
                patient.Province = patientDto.Province;
            }

            if (!string.IsNullOrEmpty(patientDto.Picture))
            {
                patient.Picture = patientDto.Picture;
            }

            await _patientRepository.UpdateAsync(patient);
        }

    }
}