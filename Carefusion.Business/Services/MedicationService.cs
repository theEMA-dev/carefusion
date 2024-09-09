using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;

namespace Carefusion.Business.Services;

public class MedicationService : IMedicationService
{
    private readonly IMedicationRepository _medicationRepository;
    private readonly IMapper _mapper;
    private readonly TimeZoneInfo _timeZone;

    public MedicationService(IMedicationRepository medicationRepository, IMapper mapper)
    {
        _medicationRepository = medicationRepository;
        _mapper = mapper;
        _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
    }

    public async Task<IEnumerable<MedicationDto>> GetMedications(int id)
    {
        var medication = await _medicationRepository.GetAllAsync();
        var filteredMedication = medication.Where(a => a.PatientId == id);
        if (!filteredMedication.Any())
            throw new InvalidOperationException("No medications found for the specified patient.");
        var medicationDtos = _mapper.Map<IEnumerable<MedicationDto>>(filteredMedication);
        return medicationDtos;
    }

    public async Task AddMedicationAsync(int id, MedicationDto medicationDto)
    {
        var medication = _mapper.Map<Medication>(medicationDto);
        medication.PatientId = id;
        medication.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _medicationRepository.AddAsync(medication);
    }

    public async Task UpdateMedicationAsync(int id, MedicationDto medicationDto)
    {
        var medication = await _medicationRepository.GetByIdAsync(id);
        if (medication == null)
        {
            throw new InvalidOperationException();
        }

        _mapper.Map(medicationDto, medication);
        medication.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _medicationRepository.UpdateAsync(medication);
    }

    public async Task<bool> DeleteMedicationAsync(int id)
    {
        var medication = await _medicationRepository.GetByIdAsync(id);
        await _medicationRepository.DeleteAsync(medication);
        return true;
    }
}