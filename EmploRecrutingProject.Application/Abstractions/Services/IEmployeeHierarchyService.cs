using EmploRecrutingProject.Domain.Entities;

namespace EmploRecrutingProject.Application.Abstractions.Services;

public interface IEmployeeHierarchyService
{
    /// <summary>
    /// Po zmianie SuperiorId na daym pracowniku, wywołaj tę metodę, aby zaktualizować hierarchię pracowników.
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task RebuildHierarchyForEmployeeAsync(Employee employee, CancellationToken cancellationToken = default);

    /// <summary>
    /// Zwraca poziom przelozonego lub null, jesli pracownik nie ma przełożonego.
    /// </summary>
    /// <returns></returns>
    Task<int?> GetSuperiorRowOfEmployeeAsync(Guid employeeId, Guid superiorId, CancellationToken cancellationToken = default);
}
