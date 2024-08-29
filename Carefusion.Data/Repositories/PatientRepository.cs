using Carefusion.Data.Interfaces;
using Carefusion.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories
{
    public class PatientRepository(ApplicationDbContext context) : IPatientRepository
    {
        public async Task<Patient> GetByIdAsync(int id)
        {
            return await context.Patients.FindAsync(id) ?? throw new InvalidOperationException();
        }

        public async Task<Patient> GetByGovIdAsync(string govId)
        {
            return await context.Patients.FirstOrDefaultAsync(p => p.GovernmentId == govId) ?? throw new InvalidOperationException();
        }

        public IQueryable<Patient> GetQuery()
        {
            return context.Patients.AsNoTracking();
        }

        public async Task AddAsync(Patient patient)
        {
            context.Patients.Add(patient);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            try
            {
                context.Patients.Update(patient);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                throw new InvalidOperationException("Cannot find hospital or it never existed.");
            }
        }
        public async Task DeleteAsync(Patient patient)
        {
            try
            {
                context.Patients.Remove(patient);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 547 })
            {
                throw new InvalidOperationException("Cannot delete the hospital because it has related departments.");
            }
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await context.Patients.ToListAsync();
        }

    }
}