using EmploRecrutingProject.Domain.Entities;

namespace EmploRecrutingProject.Application.Abstractions.Repositories;

public interface IEmployeeRepository
{
    Task<Employee> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken = default);
    Task Create(Employee employee, CancellationToken cancellationToken = default);
    IQueryable<Employee> Query(CancellationToken cancellationToken = default);
}
