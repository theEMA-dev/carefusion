using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories;

public class MedicationRepository(ApplicationDbContext context) : IMedicationRepository
{
    public async Task AddAsync(Medication medication)
    {
        context.Medications.Add(medication);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Medication medication)
    {
        context.Medications.Remove(medication);
        await context.SaveChangesAsync();
    }

    public async Task<Medication> GetByIdAsync(int id)
    {
        return await context.Medications.FindAsync(id) ?? throw new InvalidOperationException("Cannot find the medications.");
    }

    public async Task UpdateAsync(Medication medication)
    {
        context.Medications.Update(medication);
        await context.SaveChangesAsync();
    }

    public async Task<List<Medication>> GetAllAsync()
    {
        return await context.Medications.ToListAsync();
    }
}