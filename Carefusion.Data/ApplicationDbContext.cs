using Microsoft.EntityFrameworkCore;
using Carefusion.Entities; // Updated namespace

namespace Carefusion.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Patient> Patients { get; init; }
    public DbSet<Practitioner> Practitioners { get; init; }
    public DbSet<Hospital> Hospitals { get; init; }
    public DbSet<Department> Departments { get; init; }
    public DbSet<Communication> Communications { get; init; }
}