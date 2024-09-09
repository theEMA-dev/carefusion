using Carefusion.Core.Entities;

namespace Carefusion.Data.Interfaces;

public interface IImmunizationRepository
{
    Task<Immunization> GetByIdAsync(int id);
    Task AddAsync(Immunization immunization);
    Task UpdateAsync(Immunization immunization);
    Task DeleteAsync(Immunization immunization);
    Task<List<Immunization>> GetAllAsync();
}