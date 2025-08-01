using EmploRecrutingProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Employee> Employees { get; }
    DbSet<Team> Teams { get; }
    DbSet<Vacation> Vacations { get; }
    DbSet<VacationPackage> VacationPackages { get; }
    DbSet<EmployeeHierarchy> EmployeeHierarchies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
