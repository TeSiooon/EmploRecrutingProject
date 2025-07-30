using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Domain.Entities;
using EmploRecrutingProject.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EmploRecrutingProjectDbContext dbContext;

    public EmployeeRepository(EmploRecrutingProjectDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task Create(Employee employee, CancellationToken cancellationToken = default)
    {
        await dbContext.Employees.AddAsync(employee, cancellationToken);
    }

    public async Task<Employee> GetByIdAsync(Guid employeeId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Employees
            .SingleOrDefaultAsync(e => e.Id == employeeId, cancellationToken)
            ?? throw new KeyNotFoundException($"Employee with ID {employeeId} not found.");
    }
}
