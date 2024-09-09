using Carefusion.Core.DTOs;

namespace Carefusion.Business.Interfaces;

public interface IImmunizationService
{
    Task<IEnumerable<ImmunizationDto>> GetImmunizations(int id);
    Task AddImmunizationAsync(int id, ImmunizationDto immunizationDto);
    Task UpdateImmunizationAsync(int id, ImmunizationDto immunizationDto);
    Task<bool> DeleteImmunizationAsync(int id);
}