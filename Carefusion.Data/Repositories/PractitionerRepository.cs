using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories;

public class PractitionerRepository(ApplicationDbContext context) : IPractitionerRepository
{
    public async Task<Practitioner> GetByIdAsync(int id)
    {
        var practitioner = await context.Practitioners.FindAsync(id);
        if (practitioner == null)
            throw new InvalidOperationException("Cannot find the practitioner.");
        var commInfo = await context.Communications
            .Where(c => c.PractitionerIdentifier == id)
            .ToListAsync();
        if (commInfo.Count > 0)
            practitioner.Communication = commInfo;

        return practitioner;
    }

    public async Task<Practitioner> GetByGovIdAsync(string govId)
    {
        var practitioner = await context.Practitioners.FirstOrDefaultAsync(p => p.GovernmentId == govId);
        if (practitioner == null)
            throw new InvalidOperationException("Cannot find the practitioner.");
        var commInfo = await context.Communications
            .Where(c => c.PractitionerIdentifier == practitioner.Identifier)
            .ToListAsync();
        if (commInfo.Count > 0)
            practitioner.Communication = commInfo;

        return practitioner;
    }

    public IQueryable<Practitioner> GetQuery()
    {
        return context.Practitioners
            .Select(p => new Practitioner
            {
                Identifier = p.Identifier,
                Name = p.Name,
                BirthDate = p.BirthDate,
                Gender = p.Gender,
                Specialty = p.Specialty,
                Title = p.Title,
                Role = p.Role,
                GovernmentId = p.GovernmentId,
                Picture = p.Picture,
                AssignedHospitalId = p.AssignedHospitalId,
                AssignedDepartmentId = p.AssignedDepartmentId,
                Active = p.Active,
                RecordUpdated = p.RecordUpdated,
                Communication = context.Communications
                    .Where(c => c.PractitionerIdentifier == p.Identifier)
                    .ToList()
            });
    }

    public async Task AddAsync(Practitioner practitioner)
    {
        context.Practitioners.Add(practitioner);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Practitioner practitioner)
    {
        try
        {
            var commRecords = context.Communications
                .Where(c => c.PractitionerIdentifier == practitioner.Identifier)
                .ToListAsync();
            context.Communications.RemoveRange(await commRecords);
            await context.SaveChangesAsync();
            context.Practitioners.Update(practitioner);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Cannot find practitioner or it never existed.");
        }
    }

    public async Task DeleteAsync(Practitioner practitioner)
    {
        try
        {
            context.Practitioners.Remove(practitioner);
            await context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Cannot delete the practitioner.");
        }
    }

    public async Task UpdateDepartmentRelation(int id)
    {
        var practitioner = await GetByIdAsync(id);
        var department = await context.Departments.FindAsync(practitioner.AssignedDepartmentId);
        var count = await context.Practitioners.CountAsync(p => department != null && p.AssignedDepartmentId == department.Identifier);

        if (department != null)
        {
            department.RegisteredPractitioners = count;

            context.Departments.Update(department);

            await context.SaveChangesAsync();
        }
    }
}