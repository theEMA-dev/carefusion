using Carefusion.Core.Entities;

namespace Carefusion.Data.Interfaces;

public interface IProcedureRepository
{
    Task<Procedure> GetByIdAsync(int id);
    Task AddAsync(Procedure procedure);
    Task UpdateAsync(Procedure procedure);
    Task DeleteAsync(Procedure procedure);
    Task<List<Procedure>> GetAllAsync();
}