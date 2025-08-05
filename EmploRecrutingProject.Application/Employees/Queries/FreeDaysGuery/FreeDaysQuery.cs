using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Application.Abstractions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Application.Employees.Queries.FreeDaysGuery;

/// <summary>
/// Task 3
/// </summary>
public class FreeDaysQuery : IRequest<int>
{
    public Guid EmployeeId { get; init; }
}
public class FreeDaysQueryHandler : IRequestHandler<FreeDaysQuery, int>
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IVacationPolicyService vacationPolicyService;
    public FreeDaysQueryHandler(IEmployeeRepository employeeRepository, IVacationPolicyService vacationPolicyService)
    {
        this.employeeRepository = employeeRepository;
        this.vacationPolicyService = vacationPolicyService;
    }
    public async Task<int> Handle(FreeDaysQuery request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.Query(cancellationToken)
                .Include(e => e.VacationPackage)
                .Include(e => e.Vacations)
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken) ?? throw new KeyNotFoundException();

        var freeDays = vacationPolicyService.CountFreeDaysForEmployee(employee, employee.Vacations.ToList(), employee.VacationPackage);

        return freeDays;
    }
}