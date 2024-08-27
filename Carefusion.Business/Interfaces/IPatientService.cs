using Carefusion.Core;
using Carefusion.Entities; // Add this namespace to use PatientDto

namespace Carefusion.Business.Interfaces
{
    public interface IPatientService
    {
        Task<PatientDto> GetPatientByIdAsync(int id);
        Task AddPatientAsync(PatientDto patientDto);
        Task UpdatePatientAsync(int id, PatientDto patientDto);
        Task<bool> DeletePatientAsync(int id);
        Task<List<Patient>> GetAllPatientsAsync();
    }
}