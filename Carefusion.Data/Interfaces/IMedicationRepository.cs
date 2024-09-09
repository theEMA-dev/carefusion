using Carefusion.Core.Entities;

namespace Carefusion.Data.Interfaces;

public interface IMedicationRepository
{
    Task<Medication> GetByIdAsync(int id);
    Task AddAsync(Medication medication);
    Task UpdateAsync(Medication medication);
    Task DeleteAsync(Medication medication);
    Task<List<Medication>> GetAllAsync();
}