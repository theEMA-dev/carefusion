using System.Threading.Tasks;
using Carefusion.Business.Interfaces;
using Carefusion.Data.Repositories;
using Carefusion.Entities;
using Carefusion.Core; // Add this namespace to use PatientDto

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
                PatientID = patient.PatientID,
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
                Gender = patientDto.Gender,
                Email = patientDto.Email,
                Telephone = patientDto.Telephone,
                Height = patientDto.Height,
                Weight = patientDto.Weight,
                BloodType = patientDto.BloodType,
                Province = patientDto.Province,
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

            // Map PatientDto to Patient entity
            patient.FirstName = patientDto.FirstName;
            patient.LastName = patientDto.LastName;
            patient.BirthDate = patientDto.BirthDate;
            patient.Gender = patientDto.Gender;
            patient.Email = patientDto.Email;
            patient.Telephone = patientDto.Telephone;
            patient.Height = patientDto.Height;
            patient.Weight = patientDto.Weight;
            patient.BloodType = patientDto.BloodType;
            patient.Province = patientDto.Province;
            patient.Picture = patientDto.Picture;

            await _patientRepository.UpdateAsync(patient);
        }

    }
}