using Carefusion.Data.Interfaces;
using Carefusion.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories
{
    public class PatientRepository(ApplicationDbContext context) : IPatientRepository
    {
        public async Task<Patient> GetByIdAsync(int id)
        {
            return await context.Patients.FindAsync(id) ?? throw new InvalidOperationException();
        }

        public async Task AddAsync(Patient patient)
        {
            context.Patients.Add(patient);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            context.Patients.Update(patient);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Patient patient)
        {
            context.Patients.Remove(patient);
            await context.SaveChangesAsync();
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await context.Patients.ToListAsync();
        }

    }
}