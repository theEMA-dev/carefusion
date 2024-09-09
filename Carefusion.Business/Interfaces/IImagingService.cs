using Carefusion.Core.DTOs;

namespace Carefusion.Business.Interfaces;

public interface IImagingService
{
    Task<IEnumerable<ImagingDto>> GetImaging(int id);
    Task AddImagingAsync(int id, ImagingDto imagingDto);
    Task UpdateImagingAsync(int id, ImagingDto imagingDto);
    Task<bool> DeleteImagingAsync(int id);
}