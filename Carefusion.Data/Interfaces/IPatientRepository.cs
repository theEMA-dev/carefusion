using Carefusion.Entities;

namespace Carefusion.Data.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> GetByIdAsync(int id);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
        Task DeleteAsync(Patient patient);
        Task<List<Patient>> GetAllAsync();
    }
}