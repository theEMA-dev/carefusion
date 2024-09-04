using Carefusion.Core;

namespace Carefusion.Business.Interfaces;

public interface IPatientService
{
    Task<PatientDto> GetPatientByIdAsync(int id);
    Task<PatientDto> GetPatientByGovId(string govId);
    Task<(IEnumerable<PatientDto> Patients, int TotalCount)> SearchPatientsAsync(string? q, BasicSort? sortFields,
        Gender? gender, BloodType? bloodType, DateOnly? birthStartDate, DateOnly? birthEndDate, int pageNumber,
        int pageSize, bool showDeceased, bool showInactive);
    Task AddPatientAsync(PatientDto patientDto);
    Task UpdatePatientAsync(int id, PatientDto patientDto);
    Task<bool> DeletePatientAsync(int id);
}