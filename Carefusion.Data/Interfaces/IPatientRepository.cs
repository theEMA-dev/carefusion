using Carefusion.Entities;

namespace Carefusion.Data.Interfaces;

public interface IPatientRepository
{
    Task<Patient> GetByIdAsync(int id);
    Task<Patient> GetByGovIdAsync(string govId);
    IQueryable<Patient> GetQuery();
    Task AddAsync(Patient patient);
    Task UpdateAsync(Patient patient);
    Task DeleteAsync(Patient patient);
}