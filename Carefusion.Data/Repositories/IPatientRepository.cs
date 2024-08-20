using Carefusion.Entities;
using System.Threading.Tasks;

namespace Carefusion.Data.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> GetByIdAsync(int id);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
    }
}