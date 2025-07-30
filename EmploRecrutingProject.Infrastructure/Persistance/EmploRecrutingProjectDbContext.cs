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
    public DbSet<EmployeeHierarchy> EmployeeHierarchies { get; set; }

    //public override int SaveChanges()
    //{
    //    UpdateEmployeeHierarchies().GetAwaiter().GetResult();
    //    return base.SaveChanges();
    //}

    //public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    //{
    //    await UpdateEmployeeHierarchies();
    //    return await base.SaveChangesAsync(cancellationToken);
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmploRecrutingProjectDbContext).Assembly);
    }


    //private async Task UpdateEmployeeHierarchies()
    //{

    //    // Bierzemy wszystkie dodane/modyfikowane Employee, które mają zmienione SuperiorId
    //    var employees = ChangeTracker.Entries<Employee>()
    //        .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified
    //            && e.Property(p => p.SuperiorId).IsModified)
    //        .Select(e => e.Entity)
    //        .ToList();

    //    foreach (var employee in employees)
    //    {
    //        // Usuwamy istniejące hierarchie dla tego pracownika
    //        var old = EmployeeHierarchies
    //            .Where(eh => eh.EmployeeId == employee.Id)
    //            .ToList();

    //        EmployeeHierarchies
    //            .RemoveRange(old);

    //        // Jesli pracownik ma przełożonego, dodajemy nową hierarchię
    //        if (employee.SuperiorId.HasValue)
    //        {
    //            var list = new List<EmployeeHierarchy>();

    //            list.Add(new EmployeeHierarchy
    //            {
    //                EmployeeId = employee.Id,
    //                SuperiorId = employee.SuperiorId.Value,
    //                RelationLevel = 1 // Zakładamy, że poziom relacji to 1 dla bezpośredniego przełożonego
    //            });

    //            var ancestors = await EmployeeHierarchies
    //                .Where(eh => eh.EmployeeId == employee.SuperiorId.Value)
    //                .ToListAsync();

    //            foreach (var ancestor in ancestors)
    //            {
    //                list.Add(new EmployeeHierarchy
    //                {
    //                    EmployeeId = employee.Id,
    //                    SuperiorId = ancestor.SuperiorId,
    //                    RelationLevel = ancestor.RelationLevel + 1 // Zwiększamy poziom relacji o 1
    //                });
    //            }

    //            await EmployeeHierarchies
    //                .AddRangeAsync(list);
    //        }
    //    }
    //}
}

