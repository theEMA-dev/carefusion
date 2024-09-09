using Carefusion.Core.DTOs;

namespace Carefusion.Business.Interfaces;

public interface IAllergyService
{
    Task<IEnumerable<AllergyDto>> GetAllergies(int id);
    Task AddAllergyAsync(int id, AllergyDto allergyDto);
    Task UpdateAllergyAsync(int id, AllergyDto allergyDto);
    Task<bool> DeleteAllergyAsync(int id);
}