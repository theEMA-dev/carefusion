using AutoMapper;
using Carefusion.Business.Interfaces;
using Carefusion.Core;
using Carefusion.Core.Utilities;
using Carefusion.Data.Interfaces;
using Carefusion.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Business.Services;

public class PractitionerService : IPractitionerService
{
    private readonly IPractitionerRepository _practitionerRepository;
    private readonly IMapper _mapper;
    private readonly TimeZoneInfo _timeZone;

    public PractitionerService(IPractitionerRepository practitionerRepository, IMapper mapper)
    {
        _practitionerRepository = practitionerRepository;
        _mapper = mapper;
        _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
    }

    public async Task<PractitionerDto> GetPractitionerByIdAsync(int id)
    {
        var practitioner = await _practitionerRepository.GetByIdAsync(id);
        return _mapper.Map<PractitionerDto>(practitioner);
    }

    public async Task<PractitionerDto> GetPractitionerByGovId(string govId)
    {
        var practitioner = await _practitionerRepository.GetByGovIdAsync(govId);
        return _mapper.Map<PractitionerDto>(practitioner);
    }

    public async Task<(IEnumerable<PractitionerDto> Practitioners, int TotalCount)> SearchPractitionerAsync(
        string? q,
        BasicSort? sortField,
        Gender? gender,
        PractitionerTitle? title,
        PractitionerSpecialty? specialty,
        DateOnly? birthStartDate,
        DateOnly? birthEndDate,
        int pageNumber,
        int pageSize,
        bool showInactive)
    {
        var query = _practitionerRepository.GetQuery();

        if (!string.IsNullOrEmpty(q))
        {
            query = query.Where(p => p.Name.Contains(q));
        }

        if (gender != null)
        {
            query = query.Where(p => p.Gender == gender.ToString());
        }

        if (title != null)
        {
            var titleValue = title.ReadDesc();
            query = query.Where(p => p.Title == titleValue);
        }

        if (specialty != null)
        {
            query = query.Where(p => p.Specialty == specialty.ToString());
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

        if (!showInactive)
        {
            query = query.Where(p => p.Active);
        }

        query = sortField is not null ? ApplySorting(query, sortField) : query.OrderBy(p => p.Identifier);
        var totalCount = await query.CountAsync();

        var practitioners = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var practitionerDtos = _mapper.Map<IEnumerable<PractitionerDto>>(practitioners);
        return (practitionerDtos, totalCount);
    }


    private static IQueryable<Practitioner> ApplySorting(IQueryable<Practitioner> query, BasicSort? sortField)
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

    public async Task AddPractitionerAsync(PractitionerDto practitionerDto)
    {
        var practitioner = _mapper.Map<Practitioner>(practitionerDto);
        practitioner.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _practitionerRepository.AddAsync(practitioner);
    }

    public async Task UpdatePractitionerAsync(int id, PractitionerDto practitionerDto)
    {
        var practitioner = await _practitionerRepository.GetByIdAsync(id);
        if (practitioner == null)
        {
            throw new InvalidOperationException();
        }

        _mapper.Map(practitionerDto, practitioner);
        practitioner.RecordUpdated = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
        await _practitionerRepository.UpdateAsync(practitioner);
    }

    public async Task<bool> DeletePractitionerAsync(int id)
    {
        var practitioner = await _practitionerRepository.GetByIdAsync(id);
        await _practitionerRepository.DeleteAsync(practitioner);
        return true;
    }
}