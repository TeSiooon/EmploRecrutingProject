using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Application.Abstractions.Services;
using EmploRecrutingProject.Domain.Entities;

namespace EmploRecrutingProject.Infrastructure.Services;

public class EmployeeHierarchyService : IEmployeeHierarchyService
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IEmployeeHierarchyRepository employeeHierarchyRepository;

    public EmployeeHierarchyService(IEmployeeHierarchyRepository employeeHierarchyRepository, IEmployeeRepository employeeRepository)
    {
        this.employeeHierarchyRepository = employeeHierarchyRepository;
        this.employeeRepository = employeeRepository;   
    }

    public async Task<int?> GetSuperiorRowOfEmployeeAsync(Guid employeeId, Guid superiorId, CancellationToken cancellationToken = default)
    {
        var record = await employeeHierarchyRepository.FindAsync(employeeId, superiorId, cancellationToken);

        return record?.RelationLevel;
    }

    public async Task RebuildHierarchyForEmployeeAsync(Employee employee, CancellationToken cancellationToken = default)
    {
        await employeeHierarchyRepository.DeleteByEmployeeIdAsync(employee.Id, cancellationToken);

        if (!employee.SuperiorId.HasValue) return;

        var list = new List<EmployeeHierarchy>
        {
            EmployeeHierarchy.Create(
            employee.Id,
            employee.SuperiorId.Value,
            1)
        };

        var ancestors = await employeeHierarchyRepository.GetByEmployeeIdAsync(employee.SuperiorId.Value, cancellationToken);

        foreach (var ancestor in ancestors)
        {
            list.Add(EmployeeHierarchy.Create(
                employee.Id,
                ancestor.SuperiorId,
                ancestor.RelationLevel + 1));
        }

        await employeeHierarchyRepository.AddRangeAsync(list, cancellationToken);
    }
}
