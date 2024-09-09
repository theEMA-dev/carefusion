using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories;

public class ProcedureRepository(ApplicationDbContext context) : IProcedureRepository
{
    public async Task AddAsync(Procedure procedure)
    {
        context.Procedures.Add(procedure);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Procedure procedure)
    {
        context.Procedures.Remove(procedure);
        await context.SaveChangesAsync();
    }

    public async Task<Procedure> GetByIdAsync(int id)
    {
        return await context.Procedures.FindAsync(id) ?? throw new InvalidOperationException("Cannot find the procedure.");
    }

    public async Task UpdateAsync(Procedure procedure)
    {
        context.Procedures.Update(procedure);
        await context.SaveChangesAsync();
    }

    public async Task<List<Procedure>> GetAllAsync()
    {
        return await context.Procedures.ToListAsync();
    }
}