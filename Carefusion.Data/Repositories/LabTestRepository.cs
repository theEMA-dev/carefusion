using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories;

public class LabTestRepository(ApplicationDbContext context) : ILabTestRepository
{
    public async Task AddAsync(LabTest labTest)
    {
        context.LabTests.Add(labTest);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(LabTest labTest)
    {
        context.LabTests.Remove(labTest);
        await context.SaveChangesAsync();
    }

    public async Task<LabTest> GetByIdAsync(int id)
    {
        return await context.LabTests.FindAsync(id) ?? throw new InvalidOperationException("Cannot find the immunization.");
    }

    public async Task UpdateAsync(LabTest labTest)
    {
        context.LabTests.Update(labTest);
        await context.SaveChangesAsync();
    }

    public async Task<List<LabTest>> GetAllAsync()
    {
        return await context.LabTests.ToListAsync();
    }
}