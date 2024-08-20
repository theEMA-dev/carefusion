using Carefusion.Data.Interfaces;
using Carefusion.Entities;

namespace Carefusion.Data.Repositories
{
    public class HospitalRepository(ApplicationDbContext context) : IHospitalRepository
    {
        public async Task<Hospital> GetByIdAsync(int id)
        {
            return await context.Hospitals.FindAsync(id) ?? throw new InvalidOperationException();
        }

        public async Task AddAsync(Hospital hospital)
        {
            context.Hospitals.Add(hospital);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Hospital hospital)
        {
            context.Hospitals.Update(hospital);
            await context.SaveChangesAsync();
        }

    }
}