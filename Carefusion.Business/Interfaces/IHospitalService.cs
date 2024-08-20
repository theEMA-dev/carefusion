using Carefusion.Core; // Add this namespace to use PatientDto

namespace Carefusion.Business.Interfaces
{
    public interface IHospitalService
    {
        Task<HospitalDto> GetHospitalByIdAsync(int id);
        Task AddHospitalAsync(HospitalDto hospitalDto); // Ensure this method is here
        Task UpdateHospitalAsync(int id, HospitalDto hospitalDto); // Ensure this method is here
    }
}