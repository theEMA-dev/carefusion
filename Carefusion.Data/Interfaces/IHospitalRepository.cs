using Carefusion.Core.Entities;

namespace Carefusion.Data.Interfaces;

public interface IHospitalRepository
{
    Task<Hospital> GetByIdAsync(int id);
    IQueryable<Hospital> GetQuery();
    Task AddAsync(Hospital hospital);
    Task UpdateAsync(Hospital hospital);
    Task DeleteAsync(Hospital hospital);
}