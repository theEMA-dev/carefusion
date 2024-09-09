using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;

namespace Carefusion.Business.Services;

public class ImagingService : IImagingService
{
    private readonly IImagingRepository _imagingRepository;
    private readonly IMapper _mapper;
    private readonly TimeZoneInfo _timeZone;

    public ImagingService(IImagingRepository imagingRepository, IMapper mapper)
    {
        _imagingRepository = imagingRepository;
        _mapper = mapper;
        _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
    }

    public async Task<IEnumerable<ImagingDto>> GetImaging(int id)
    {
        var imaging = await _imagingRepository.GetAllAsync();
        var filteredImaging = imaging.Where(a => a.PatientId == id);
        if (!filteredImaging.Any())
            throw new InvalidOperationException("No imaging found for the specified patient.");
        var imagingDtos = _mapper.Map<IEnumerable<ImagingDto>>(filteredImaging);
        return imagingDtos;
    }

    public async Task AddImagingAsync(int id, ImagingDto imagingDto)
    {
        var imaging = _mapper.Map<Imaging>(imagingDto);
        imaging.PatientId = id;
        imaging.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _imagingRepository.AddAsync(imaging);
    }

    public async Task UpdateImagingAsync(int id, ImagingDto imagingDto)
    {
        var imaging = await _imagingRepository.GetByIdAsync(id);
        if (imaging == null)
        {
            throw new InvalidOperationException();
        }

        _mapper.Map(imagingDto, imaging);
        imaging.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _imagingRepository.UpdateAsync(imaging);
    }

    public async Task<bool> DeleteImagingAsync(int id)
    {
        var imaging = await _imagingRepository.GetByIdAsync(id);
        await _imagingRepository.DeleteAsync(imaging);
        return true;
    }
}