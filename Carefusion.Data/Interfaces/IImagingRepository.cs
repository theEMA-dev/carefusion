using Carefusion.Core.Entities;

namespace Carefusion.Data.Interfaces;

public interface IImagingRepository
{
    Task<Imaging> GetByIdAsync(int id);
    Task AddAsync(Imaging imaging);
    Task UpdateAsync(Imaging imaging);
    Task DeleteAsync(Imaging imaging);
    Task<List<Imaging>> GetAllAsync();
}