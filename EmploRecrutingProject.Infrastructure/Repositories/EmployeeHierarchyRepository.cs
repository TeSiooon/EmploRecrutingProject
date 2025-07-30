using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Domain.Entities;
using EmploRecrutingProject.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Infrastructure.Repositories;

public class EmployeeHierarchyRepository : IEmployeeHierarchyRepository
{
    private readonly EmploRecrutingProjectDbContext dbContext;

    public EmployeeHierarchyRepository(EmploRecrutingProjectDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddRangeAsync(IEnumerable<EmployeeHierarchy> entries, CancellationToken cancellationToken = default)
    {
        await dbContext.EmployeeHierarchies.AddRangeAsync(entries, cancellationToken);
    }

    public async Task DeleteByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        var toDelete = await dbContext.EmployeeHierarchies
            .Where(eh => eh.EmployeeId == employeeId)
            .ToListAsync(cancellationToken);

        if(toDelete.Any())
             dbContext.EmployeeHierarchies
                .RemoveRange(toDelete);
    }

    public async Task<EmployeeHierarchy> FindAsync(Guid employeeId, Guid superiorId, CancellationToken cancellationToken = default)
    {
        return await dbContext.EmployeeHierarchies
            .AsNoTracking()
            .SingleOrDefaultAsync(
                x => x.EmployeeId == employeeId && x.SuperiorId == superiorId, cancellationToken) ?? throw new KeyNotFoundException();
    }

    public async Task<List<EmployeeHierarchy>> GetByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        return await dbContext.EmployeeHierarchies
            .AsNoTracking()
            .Where(eh => eh.EmployeeId == employeeId)
            .ToListAsync(cancellationToken);
    }
}
