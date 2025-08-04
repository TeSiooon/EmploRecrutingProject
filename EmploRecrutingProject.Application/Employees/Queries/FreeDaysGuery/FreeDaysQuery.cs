using EmploRecrutingProject.Application.Abstractions;
using EmploRecrutingProject.Application.Abstractions.Services;
using EmploRecrutingProject.Domain.Entities;
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
    private readonly IApplicationDbContext dbContext;
    private readonly IVacationPolicyService vacationPolicyService;
    public FreeDaysQueryHandler(IApplicationDbContext dbContext, IVacationPolicyService vacationPolicyService)
    {
        this.dbContext = dbContext;
        this.vacationPolicyService = vacationPolicyService;
    }
    public async Task<int> Handle(FreeDaysQuery request, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Employees
            .AsNoTracking()
            .Include(e => e.VacationPackage)
            .Include(e => e.Vacations)
            .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken) ?? throw new KeyNotFoundException();

        var freeDays = vacationPolicyService.CountFreeDaysForEmployee(employee, employee.Vacations.ToList(), employee.VacationPackage);

        return freeDays;
    }
}