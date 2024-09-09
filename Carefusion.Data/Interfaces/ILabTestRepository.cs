using Carefusion.Core.Entities;

namespace Carefusion.Data.Interfaces;

public interface ILabTestRepository
{
    Task<LabTest> GetByIdAsync(int id);
    Task AddAsync(LabTest labTest);
    Task UpdateAsync(LabTest labTest);
    Task DeleteAsync(LabTest labTest);
    Task<List<LabTest>> GetAllAsync();
}