using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories;

public class DepartmentRepository(ApplicationDbContext context) : IDepartmentRepository
{
    public async Task AddAsync(Department department)
    {
        context.Departments.Add(department);
        await context.SaveChangesAsync();
    }   

    public async Task DeleteAsync(Department department) 
    {
        context.Departments.Remove(department);
        await context.SaveChangesAsync();
    }

    public async Task<Department> GetByIdAsync(int id)
    {
        return await context.Departments.FindAsync(id) ?? throw new InvalidOperationException("Cannot find the department.");
    }

    public async Task UpdateAsync(Department department)
    {
        context.Departments.Update(department);
        await context.SaveChangesAsync();
    }
    public async Task<List<Department>> GetAllAsync()
    {
        return await context.Departments.ToListAsync();
    }
}