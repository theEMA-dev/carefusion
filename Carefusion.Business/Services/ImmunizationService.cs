using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;

namespace Carefusion.Business.Services;

public class ImmunizationService : IImmunizationService
{
    private readonly IImmunizationRepository _immunizationRepository;
    private readonly IMapper _mapper;
    private readonly TimeZoneInfo _timeZone;

    public ImmunizationService(IImmunizationRepository immunizationRepository, IMapper mapper)
    {
        _immunizationRepository = immunizationRepository;
        _mapper = mapper;
        _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
    }

    public async Task<IEnumerable<ImmunizationDto>> GetImmunizations(int id)
    {
        var immunization = await _immunizationRepository.GetAllAsync();
        var filteredImmunization = immunization.Where(a => a.PatientId == id);
        if (!filteredImmunization.Any())
            throw new InvalidOperationException("No immunizations found for the specified patient.");
        var immunizationDtos = _mapper.Map<IEnumerable<ImmunizationDto>>(filteredImmunization);
        return immunizationDtos;
    }

    public async Task AddImmunizationAsync(int id, ImmunizationDto immunizationDto)
    {
        var immunization = _mapper.Map<Immunization>(immunizationDto);
        immunization.PatientId = id;
        immunization.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _immunizationRepository.AddAsync(immunization);
    }

    public async Task UpdateImmunizationAsync(int id, ImmunizationDto immunizationDto)
    {
        var immunization = await _immunizationRepository.GetByIdAsync(id);
        if (immunization == null)
        {
            throw new InvalidOperationException();
        }

        _mapper.Map(immunizationDto, immunization);
        immunization.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _immunizationRepository.UpdateAsync(immunization);
    }

    public async Task<bool> DeleteImmunizationAsync(int id)
    {
        var immunization = await _immunizationRepository.GetByIdAsync(id);
        await _immunizationRepository.DeleteAsync(immunization);
        return true;
    }
}