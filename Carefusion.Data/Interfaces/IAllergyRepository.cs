using Carefusion.Core.Entities;

namespace Carefusion.Data.Interfaces;

public interface IAllergyRepository
{
    Task<Allergy> GetByIdAsync(int id);
    Task AddAsync(Allergy allergy);
    Task UpdateAsync(Allergy allergy);
    Task DeleteAsync(Allergy allergy);
    Task<List<Allergy>> GetAllAsync();
}