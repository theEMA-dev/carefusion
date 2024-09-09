using Carefusion.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Carefusion.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients { get; init; }
    public DbSet<Practitioner> Practitioners { get; init; }
    public DbSet<Hospital> Hospitals { get; init; }
    public DbSet<Department> Departments { get; init; }
    public DbSet<Communication> Communications { get; init; }
    public DbSet<Allergy> Allergies { get; init; }
    public DbSet<Imaging> Imaging { get; init; }
    public DbSet<Immunization> Immunizations { get; init; }
    public DbSet<LabTest> LabTests { get; init; }
    public DbSet<Medication> Medications { get; init; }
    public DbSet<Procedure> Procedures { get; init; }
    public DbSet<Appointment> Appointments { get; init; }
}
