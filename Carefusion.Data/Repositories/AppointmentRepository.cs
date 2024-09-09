using Carefusion.Core.DTOs;
using Carefusion.Core.Entities;
using Carefusion.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data.Repositories;

public class AppointmentRepository(ApplicationDbContext context) : IAppointmentRepository
{
    public async Task AddAsync(Appointment appointment)
    {
        context.Appointments.Add(appointment);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Appointment appointment)
    {
        context.Appointments.Remove(appointment);
        await context.SaveChangesAsync();
    }

    public async Task<Appointment> GetByIdAsync(int id)
    {
        return await context.Appointments.FindAsync(id) ?? throw new InvalidOperationException("Cannot find the immunization.");
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        context.Appointments.Update(appointment);
        await context.SaveChangesAsync();
    }

    public async Task<List<Appointment>> GetAllAsync()
    {
        return await context.Appointments.ToListAsync();
    }

    public async Task<MedicalHistoryDto> GetMedicalHistoryAsync(int id)
    {
        var patient = await context.Patients.FindAsync(id);
        var appointments = await context.Appointments
            .Where(a => a.PatientId == id)
            .ToListAsync();
        var allergies = await context.Allergies
            .Where(a => a.PatientId == id)
            .ToListAsync();
        var labTests = await context.LabTests
            .Where(l => l.PatientId == id)
            .ToListAsync();
        var imaging = await context.Imaging
            .Where(i => i.PatientId == id)
            .ToListAsync();
        var immunizations = await context.Immunizations
            .Where(i => i.PatientId == id)
            .ToListAsync();
        var medications = await context.Medications
            .Where(m => m.PatientId == id)
            .ToListAsync();
        var procedures = await context.Procedures
            .Where(p => p.PatientId == id)
            .ToListAsync();

        var medicalHistory = new MedicalHistoryDto
        {
            Patient = patient,
            Appointments = appointments,
            Allergies = allergies,
            LabTests = labTests,
            Imaging = imaging,
            Immunizations = immunizations,
            Medications = medications,
            Procedures = procedures
        };


        return medicalHistory;
    }
}