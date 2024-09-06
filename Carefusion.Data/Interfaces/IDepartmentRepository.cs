using Carefusion.Entities;

namespace Carefusion.Data.Interfaces;

public interface IDepartmentRepository
{
    Task<Department> GetByIdAsync(int id);
    Task AddAsync(Department department);
    Task UpdateAsync(Department department);
    Task DeleteAsync(Department department);
    Task<List<Department>> GetAllAsync();
}