using Microsoft.EntityFrameworkCore;
using Carefusion.Entities;
using System.Threading.Tasks;

namespace Carefusion.Data.Repositories
{
    public class PatientRepository(ApplicationDbContext context) : IPatientRepository
    {
        public async Task<Patient> GetByIdAsync(int id)
        {
            return await context.Patients.FindAsync(id);
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

    }
}