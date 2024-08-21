using Carefusion.Entities;

namespace Carefusion.Data.Interfaces
{
    public interface IHospitalRepository
    {
        Task<Hospital> GetByIdAsync(int id);
        Task AddAsync(Hospital hospital);
        Task UpdateAsync(Hospital hospital);
        Task DeleteAsync(Hospital hospital);
    }
}