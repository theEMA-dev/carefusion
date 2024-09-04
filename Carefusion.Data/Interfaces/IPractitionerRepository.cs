using Carefusion.Entities;

namespace Carefusion.Data.Interfaces;

public interface IPractitionerRepository
{
    Task<Practitioner> GetByIdAsync(int id);
    Task<Practitioner> GetByGovIdAsync(string govId);
    IQueryable<Practitioner> GetQuery();
    Task AddAsync(Practitioner practitioner);
    Task UpdateAsync(Practitioner practitioner);
    Task DeleteAsync(Practitioner practitioner);
}