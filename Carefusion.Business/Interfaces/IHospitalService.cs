using Carefusion.Core;
using Carefusion.Core.Criterias;

namespace Carefusion.Business.Interfaces
{
    public interface IHospitalService
    {
        Task<HospitalDto> GetHospitalByIdAsync(int id);
        Task<(IEnumerable<HospitalDto> Hospitals, int TotalCount)> SearchHospitalsAsync(string searchTerm, HospitalFilterCriteria? filterCriteria, HospitalSortCriteria? sortCriteria, int pageNumber, int pageSize);
        Task AddHospitalAsync(HospitalDto hospitalDto);
        Task UpdateHospitalAsync(int id, HospitalDto hospitalDto);
        Task<bool> DeleteHospitalAsync(int id);
        Task<(IEnumerable<HospitalDto> Hospitals, int TotalCount)> GetAllHospitalsAsync(int pageNumber, int pageSize);
    }
}