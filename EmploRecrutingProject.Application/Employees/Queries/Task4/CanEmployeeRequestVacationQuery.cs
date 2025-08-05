using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Application.Abstractions.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Application.Employees.Queries.Task4;

public class CanEmployeeRequestVacationQuery : IRequest<bool>
{
    public Guid EmployeeId { get; init; }
}
public class CanEmployeeRequestVacationQueryHandler : IRequestHandler<CanEmployeeRequestVacationQuery, bool>
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IVacationPolicyService vacationPolicyService;

    public CanEmployeeRequestVacationQueryHandler(IEmployeeRepository employeeRepository, IVacationPolicyService vacationPolicyService)
    {
        this.employeeRepository = employeeRepository;
        this.vacationPolicyService = vacationPolicyService;
    }

    public async Task<bool> Handle(CanEmployeeRequestVacationQuery request, CancellationToken cancellationToken)
    {
        var employee = await employeeRepository.Query(cancellationToken)
            .Include(e => e.VacationPackage)
            .Include(e => e.Vacations)
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken) ?? throw new KeyNotFoundException();

        var canEmployeeRequestVacations = vacationPolicyService.IfEmployeeCanRequestVacation(employee, employee.Vacations,
            employee.VacationPackage);

        return canEmployeeRequestVacations;
    }
}
