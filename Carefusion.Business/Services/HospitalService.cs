using Carefusion.Business.Interfaces;
using Carefusion.Core;
using Carefusion.Data.Interfaces;
using Carefusion.Entities;

namespace Carefusion.Business.Services
{
    public class HospitalService(IHospitalRepository hospitalRepository) : IHospitalService
    {
        public async Task<HospitalDto> GetHospitalByIdAsync(int id)
        {
            var hospital = await hospitalRepository.GetByIdAsync(id);
            return new HospitalDto
            {
                HospitalId = hospital.HospitalId,
                HospitalName = hospital.HospitalName,
                HospitalType = hospital.HospitalType,
                Province = hospital.Province,
                District = hospital.District,
                FullAdress = hospital.FullAdress
            };
        }

        public async Task AddHospitalAsync(HospitalDto hospitalDto)
        {
            var hospital = new Hospital
            {
                HospitalName = hospitalDto.HospitalName,
                HospitalType = hospitalDto.HospitalType,
                Province = hospitalDto.Province,
                District = hospitalDto.District,
                FullAdress = hospitalDto.FullAdress
            };
            await hospitalRepository.AddAsync(hospital);
        }
        public async Task UpdateHospitalAsync(int id, HospitalDto hospitalDto)
        {
            var hospital = await hospitalRepository.GetByIdAsync(id);
            if (hospital == null)
            {
                throw new Utilities.NotFoundException("Patient not found.");
            }

            if (!string.IsNullOrEmpty(hospitalDto.HospitalName))
            {
                hospital.HospitalName = hospitalDto.HospitalName;
            }
            if (!string.IsNullOrEmpty(hospitalDto.HospitalType))
            {
                hospital.HospitalType = hospitalDto.HospitalType;
            }
            if (!string.IsNullOrEmpty(hospitalDto.Province))
            {
                hospital.Province = hospitalDto.Province;
            }
            if (!string.IsNullOrEmpty(hospitalDto.District))
            {
                hospital.District = hospitalDto.District;
            }
            if (!string.IsNullOrEmpty(hospitalDto.FullAdress))
            {
                hospital.FullAdress = hospitalDto.FullAdress;
            }

            await hospitalRepository.UpdateAsync(hospital);
        }
    }
}
