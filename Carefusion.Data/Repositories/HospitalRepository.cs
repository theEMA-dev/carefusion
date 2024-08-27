using Carefusion.Core.Criterias;
using Carefusion.Data.Interfaces;
using Carefusion.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
namespace Carefusion.Data.Repositories
{
    public class HospitalRepository(ApplicationDbContext context) : IHospitalRepository
    {
        public async Task<Hospital> GetByIdAsync(int id)
        {
            return await context.Hospitals.FindAsync(id) ?? throw new InvalidOperationException("Cannot find the hospital.");
        }

        public IQueryable<Hospital> SearchHospitals(string searchTerm, HospitalFilterCriteria? filterCriteria, HospitalSortCriteria? sortCriteria)
        {
            var query = context.Hospitals.AsNoTracking();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(h => h.Affiliation != null && (h.Name.Contains(searchTerm) || h.Code.Contains(searchTerm) || h.Affiliation.Contains(searchTerm)));
            }

            if (filterCriteria != null)
            {
                if (filterCriteria.type != null && filterCriteria.type.Any())
                {
                    query = query.Where(h => filterCriteria.type.Contains(h.Type));
                }

                if (!filterCriteria.showInactive)
                {
                    query = query.Where(h => h.Active);
                }
            }

            if (sortCriteria != null)
            {
                if (sortCriteria.sortByNumberOfBeds)
                {
                    query = query.OrderByDescending(h => h.NumberOfBeds);
                }
                else if (sortCriteria.sortByEmergencyServices)
                {
                    query = query.OrderByDescending(h => h.EmergencyServices);
                }
                else if (sortCriteria.sortByCity)
                {
                    query = query.OrderBy(h => h.City);
                }
                else if (sortCriteria.sortByDistrict)
                {
                    query = query.OrderBy(h => h.District);
                }
            }
            return query;
        }

        public async Task AddAsync(Hospital hospital)
        {
            context.Hospitals.Add(hospital);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hospital hospital)
        {
            try
            {
                context.Hospitals.Update(hospital);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Cannot find hospital or it never existed.");
            }
        }

        public async Task DeleteAsync(Hospital hospital)
        {
            try
            {
                context.Hospitals.Remove(hospital);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 547 })
            {
                // Foreign key constraint violation
                throw new InvalidOperationException("Cannot delete the hospital because it has related departments.");
            }
        }

        public async Task<(IEnumerable<Hospital> Hospitals, int TotalCount)> GetAllAsync(int pageNumber, int pageSize)
        {
            var query = context.Hospitals.AsNoTracking();
            var totalCount = await query.CountAsync();

            var hospitals = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (hospitals, totalCount);
        }


    }
}