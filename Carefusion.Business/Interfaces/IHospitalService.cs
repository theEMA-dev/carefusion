using Carefusion.Core;

namespace Carefusion.Business.Interfaces
{
    public interface IHospitalService
    {
        Task<HospitalDto> GetHospitalByIdAsync(int id);
        Task<(IEnumerable<HospitalDto> Hospitals, int TotalCount)> SearchHospitalsAsync(string? q, string[]? type, string[]? sortFields, int pageNumber, int pageSize, bool showInactive);
        Task AddHospitalAsync(HospitalDto hospitalDto);
        Task UpdateHospitalAsync(int id, HospitalDto hospitalDto);
        Task<bool> DeleteHospitalAsync(int id);
    }
}