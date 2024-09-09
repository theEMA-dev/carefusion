using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Core;
using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
using Carefusion.Core.Utilities;
using Carefusion.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Business.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IPractitionerService _practitionerService;
    private readonly IMapper _mapper;
    private readonly TimeZoneInfo _timeZone;

    public PatientService(IPatientRepository patientRepository, IPractitionerService practitionerService, IMapper mapper)
    {
        _patientRepository = patientRepository;
        _practitionerService = practitionerService;
        _mapper = mapper;
        _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
    }

    public async Task<PatientDto> GetPatientByIdAsync(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        var patientDto = _mapper.Map<PatientDto>(patient);
        patientDto.PractitionerName = await _practitionerService.GetPractitionerNameById(patient.AssignedPractitionerId);
        return patientDto;
    }

    public async Task<PatientDto> GetPatientByGovId(string govId)
    {
        var patient = await _patientRepository.GetByGovIdAsync(govId);
        var patientDto = _mapper.Map<PatientDto>(patient);
        patientDto.PractitionerName = await _practitionerService.GetPractitionerNameById(patient.AssignedPractitionerId);
        return patientDto;
    }

    public async Task<(IEnumerable<PatientDto> Patients, int TotalCount)> SearchPatientsAsync(
        string? q,
        BasicSort? sortField,
        Gender? gender,
        BloodType? bloodType,
        DateOnly? birthStartDate,
        DateOnly? birthEndDate,
        int pageNumber,
        int pageSize,
        bool showDeceased,
        bool showInactive)
    {
        var query = _patientRepository.GetQuery();

        if (!string.IsNullOrEmpty(q))
        {
            query = query.Where(p => p.Name.Contains(q));
        }

        if (gender != null)
        {
            query = query.Where(p => p.Gender == gender.ToString());
        }

        if (bloodType != null)
        {
            var bloodTypeValue = bloodType.ReadDesc();
            query = query.Where(p => p.BloodType == bloodTypeValue);
        }

        if (birthStartDate != null && birthEndDate == null)
        {
            query = query.Where(p => p.BirthDate >= birthStartDate);
        }

        if (birthStartDate != null && birthEndDate != null)
        {
            query = query.Where(p => p.BirthDate >= birthStartDate && p.BirthDate <= birthEndDate);
        }

        if (birthStartDate == null && birthEndDate != null)
        {
            query = query.Where(p => p.BirthDate <= birthEndDate);
        }

        if (!showDeceased)
        {
            query = query.Where(p => !p.Deceased);
        }

        if (!showInactive)
        {
            query = query.Where(p => p.Active);
        }

        query = sortField is not null ? ApplySorting(query, sortField) : query.OrderBy(p => p.Identifier);
        var totalCount = await query.CountAsync();

        var patients = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var patientDtos = _mapper.Map<IEnumerable<PatientDto>>(patients);

        var enumerable = patientDtos.ToList();
        foreach (var patientDto in enumerable)
        {
            patientDto.PractitionerName = await _practitionerService.GetPractitionerNameById(patientDto.AssignedPractitionerId);
        }

        return (enumerable, totalCount);
    }

    private static IQueryable<Patient> ApplySorting(IQueryable<Patient> query, BasicSort? sortField)
    {
        return sortField switch
        {
            BasicSort.alphabeticalAsc => query.OrderBy(p => p.Name),
            BasicSort.alphabeticalDesc => query.OrderByDescending(p => p.Name),
            BasicSort.newest => query.OrderByDescending(p => p.RecordUpdated),
            BasicSort.oldest => query.OrderBy(p => p.RecordUpdated),
            _ => throw new ArgumentOutOfRangeException(nameof(sortField), sortField, null)
        };
    }

    public async Task AddPatientAsync(PatientDto patientDto)
    {
        var patient = _mapper.Map<Patient>(patientDto);
        patient.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _patientRepository.AddAsync(patient);
    }

    public async Task UpdatePatientAsync(int id, PatientDto patientDto)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        if (patient == null)
        {
            throw new InvalidOperationException();
        }

        _mapper.Map(patientDto, patient);
        patient.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _patientRepository.UpdateAsync(patient);
    }

    public async Task<bool> DeletePatientAsync(int id)
    {
        var patient = await _patientRepository.GetByIdAsync(id);
        await _patientRepository.DeleteAsync(patient);
        return true;
    }
}