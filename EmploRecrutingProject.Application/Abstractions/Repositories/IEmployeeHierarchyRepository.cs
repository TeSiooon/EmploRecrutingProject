using EmploRecrutingProject.Domain.Entities;

namespace EmploRecrutingProject.Application.Abstractions.Repositories;

public interface IEmployeeHierarchyRepository
{
    Task<List<EmployeeHierarchy>> GetByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task DeleteByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<EmployeeHierarchy> entries, CancellationToken cancellationToken = default);
    Task<EmployeeHierarchy> FindAsync(Guid employeeId, Guid superiorId, CancellationToken cancellationToken = default);
}
