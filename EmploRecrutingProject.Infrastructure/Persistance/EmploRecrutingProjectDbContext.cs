using EmploRecrutingProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Infrastructure.Persistance;

public class EmploRecrutingProjectDbContext : DbContext
{
    public EmploRecrutingProjectDbContext(DbContextOptions<EmploRecrutingProjectDbContext> options)
        : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<Vacation> Vacations { get; set; }
    public DbSet<VacationPackage> VacationPackages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmploRecrutingProjectDbContext).Assembly);
    }
}

