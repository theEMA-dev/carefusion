using Carefusion.Core.Criterias;
using Carefusion.Entities;

namespace Carefusion.Data.Interfaces
{
    public interface IHospitalRepository
    {
        Task<Hospital> GetByIdAsync(int id);
        IQueryable<Hospital> SearchHospitals(string searchTerm, HospitalFilterCriteria? filterCriteria, HospitalSortCriteria? sortCriteria);
        Task AddAsync(Hospital hospital);
        Task UpdateAsync(Hospital hospital);
        Task DeleteAsync(Hospital hospital);
        Task<(IEnumerable<Hospital> Hospitals, int TotalCount)> GetAllAsync(int pageNumber, int pageSize);

    }
}