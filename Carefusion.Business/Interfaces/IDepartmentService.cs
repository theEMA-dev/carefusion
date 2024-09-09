using Carefusion.Core.DTOs;

namespace Carefusion.Business.Interfaces;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetDepartments(int id);
    Task AddDepartmentAsync(int id, DepartmentDto departmentDto);
    Task UpdateDepartmentAsync(int id, DepartmentDto departmentDto);
    Task<bool> DeleteDepartmentAsync(int id);
    Task<string?> GetDepartmentNameById(int? id);
}