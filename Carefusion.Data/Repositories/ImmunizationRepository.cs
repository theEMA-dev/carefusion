using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories;

public class ImmunizationRepository(ApplicationDbContext context) : IImmunizationRepository
{
    public async Task AddAsync(Immunization immunization)
    {
        context.Immunizations.Add(immunization);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Immunization immunization)
    {
        context.Immunizations.Remove(immunization);
        await context.SaveChangesAsync();
    }

    public async Task<Immunization> GetByIdAsync(int id)
    {
        return await context.Immunizations.FindAsync(id) ?? throw new InvalidOperationException("Cannot find the immunization.");
    }

    public async Task UpdateAsync(Immunization immunization)
    {
        context.Immunizations.Update(immunization);
        await context.SaveChangesAsync();
    }

    public async Task<List<Immunization>> GetAllAsync()
    {
        return await context.Immunizations.ToListAsync();
    }
}