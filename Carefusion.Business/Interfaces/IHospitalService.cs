using Carefusion.Core;
using Carefusion.Core.DTOs;

namespace Carefusion.Business.Interfaces;

public interface IHospitalService
{
    Task<HospitalDto> GetHospitalByIdAsync(int id);
    Task<(IEnumerable<HospitalDto> Hospitals, int TotalCount)> SearchHospitalsAsync(string? q, string[]? type, HospitalSort? sortFields, bool? emergencyServices, int pageNumber, int pageSize, bool showInactive);
    Task AddHospitalAsync(HospitalDto hospitalDto);
    Task UpdateHospitalAsync(int id, HospitalDto hospitalDto);
    Task<bool> DeleteHospitalAsync(int id);
    Task<string?> GetHospitalNameById(int? id);
}