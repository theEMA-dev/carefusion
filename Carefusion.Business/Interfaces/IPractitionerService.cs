using Carefusion.Core;

namespace Carefusion.Business.Interfaces;

public interface IPractitionerService
{
    Task<PractitionerDto> GetPractitionerByIdAsync(int id);
    Task<PractitionerDto> GetPractitionerByGovId(string govId);
    Task<(IEnumerable<PractitionerDto> Practitioners, int TotalCount)> SearchPractitionerAsync(string? q, BasicSort? sortFields,
        Gender? gender, PractitionerTitle? title, PractitionerSpecialty? specialty, DateOnly? birthStartDate, DateOnly? birthEndDate, int pageNumber,
        int pageSize, bool showInactive);
    Task AddPractitionerAsync(PractitionerDto practitionerDto);
    Task UpdatePractitionerAsync(int id, PractitionerDto practitionerDto);
    Task<bool> DeletePractitionerAsync(int id);
    Task<string?> GetPractitionerNameById(int? id);
}