using Carefusion.Data.Interfaces;
using Carefusion.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories;

public class PatientRepository(ApplicationDbContext context) : IPatientRepository
{
    public async Task<Patient> GetByIdAsync(int id)
    {
        var patient = await context.Patients.FindAsync(id);
        if (patient == null)
            throw new InvalidOperationException("Cannot find the patient.");
        var commInfo = await context.Communications
            .Where(c => c.PatientIdentifier == id)
            .ToListAsync();
        if (commInfo.Count > 0)
            patient.Communication = commInfo;

        return patient;
    }

    public async Task<Patient> GetByGovIdAsync(string govId)
    {
        var patient = await context.Patients.FirstOrDefaultAsync(p => p.GovernmentId == govId);
        if (patient == null)
            throw new InvalidOperationException("Cannot find the patient.");
        var commInfo = await context.Communications
            .Where(c => c.PatientIdentifier == patient.Identifier)
            .ToListAsync();
        if (commInfo.Count > 0)
            patient.Communication = commInfo;

        return patient;
    }

    public IQueryable<Patient> GetQuery()
    {
        return context.Patients
            .Select(p => new Patient
            {
                Identifier = p.Identifier,
                Name = p.Name,
                BirthDate = p.BirthDate,
                Gender = p.Gender,
                BloodType = p.BloodType,
                GovernmentId = p.GovernmentId,
                Picture = p.Picture,
                AssignedPractitioner = p.AssignedPractitioner,
                HealthcareProvider = p.HealthcareProvider,
                Active = p.Active,
                Deceased = p.Deceased,
                RecordUpdated = p.RecordUpdated,
                Communication = context.Communications
                    .Where(c => c.PatientIdentifier == p.Identifier)
                    .ToList()
            });
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
            var commRecords = context.Communications
                .Where(c => c.PatientIdentifier == patient.Identifier)
                .ToListAsync();
            context.Communications.RemoveRange(await commRecords);
            await context.SaveChangesAsync();
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
}