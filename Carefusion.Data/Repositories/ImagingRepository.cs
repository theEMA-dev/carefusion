using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories;

public class ImagingRepository(ApplicationDbContext context) : IImagingRepository
{
    public async Task AddAsync(Imaging imaging)
    {
        context.Imaging.Add(imaging);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Imaging imaging)
    {
        context.Imaging.Remove(imaging);
        await context.SaveChangesAsync();
    }

    public async Task<Imaging> GetByIdAsync(int id)
    {
        return await context.Imaging.FindAsync(id) ?? throw new InvalidOperationException("Cannot find the allergy.");
    }

    public async Task UpdateAsync(Imaging imaging)
    {
        context.Imaging.Update(imaging);
        await context.SaveChangesAsync();
    }

    public async Task<List<Imaging>> GetAllAsync()
    {
        return await context.Imaging.ToListAsync();
    }
}