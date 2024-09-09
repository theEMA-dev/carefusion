using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories;

public class AllergyRepository(ApplicationDbContext context) : IAllergyRepository
{
    public async Task AddAsync(Allergy allergy)
    {
        context.Allergies.Add(allergy);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Allergy allergy)
    {
        context.Allergies.Remove(allergy);
        await context.SaveChangesAsync();
    }

    public async Task<Allergy> GetByIdAsync(int id)
    {
        return await context.Allergies.FindAsync(id) ?? throw new InvalidOperationException("Cannot find the allergy.");
    }

    public async Task UpdateAsync(Allergy allergy)
    {
        context.Allergies.Update(allergy);
        await context.SaveChangesAsync();
    }

    public async Task<List<Allergy>> GetAllAsync()
    {
        return await context.Allergies.ToListAsync();
    }
} 