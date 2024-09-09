using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories;

public class HospitalRepository(ApplicationDbContext context) : IHospitalRepository
{
    public async Task<Hospital> GetByIdAsync(int id)
    { 
        return await context.Hospitals.FindAsync(id) ?? throw new InvalidOperationException("Cannot find the hospital.");
    }

    public IQueryable<Hospital> GetQuery()
    {
        return context.Hospitals.AsNoTracking();
    }

    public async Task AddAsync(Hospital hospital)
    {
        context.Hospitals.Add(hospital);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Hospital hospital)
    {
        try
        {
            context.Hospitals.Update(hospital);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Cannot find hospital or it never existed.");
        }
    }

    public async Task DeleteAsync(Hospital hospital)
    {
        try
        {
            context.Hospitals.Remove(hospital);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 547 })
        {
            throw new InvalidOperationException("Cannot delete the hospital because it has related departments.");
        }
    }
}