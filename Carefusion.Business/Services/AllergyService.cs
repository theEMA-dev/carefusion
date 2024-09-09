using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;

namespace Carefusion.Business.Services;

public class AllergyService : IAllergyService
{
    private readonly IAllergyRepository _allergyRepository;
    private readonly IMapper _mapper;
    private readonly TimeZoneInfo _timeZone;

    public AllergyService(IAllergyRepository allergyRepository, IMapper mapper)
    {
        _allergyRepository = allergyRepository;
        _mapper = mapper;
        _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
    }

    public async Task<IEnumerable<AllergyDto>> GetAllergies(int id)
    {
        var allergies = await _allergyRepository.GetAllAsync();
        var filteredAllergies = allergies.Where(a => a.PatientId == id);
        if (!filteredAllergies.Any())
            throw new InvalidOperationException("No allergies found for the specified patient.");
        var allergyDtos = _mapper.Map<IEnumerable<AllergyDto>>(filteredAllergies);
        return allergyDtos;
    }

    public async Task AddAllergyAsync(int id, AllergyDto allergyDto)
    {
        var allergy = _mapper.Map<Allergy>(allergyDto);
        allergy.PatientId = id;
        allergy.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _allergyRepository.AddAsync(allergy);
    }

    public async Task UpdateAllergyAsync(int id, AllergyDto allergyDto)
    {
        var allergy = await _allergyRepository.GetByIdAsync(id);
        if (allergy == null)
        {
            throw new InvalidOperationException();
        }

        _mapper.Map(allergyDto, allergy);
        allergy.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _allergyRepository.UpdateAsync(allergy);
    }

    public async Task<bool> DeleteAllergyAsync(int id)
    {
        var allergy = await _allergyRepository.GetByIdAsync(id);
        await _allergyRepository.DeleteAsync(allergy);
        return true;
    }
}