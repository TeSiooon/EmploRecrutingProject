using EmploRecrutingProject.Application.Abstractions;
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
    internal sealed class FreeDaysQueryHandler : IRequestHandler<FreeDaysQuery, int>
    {
        private readonly IApplicationDbContext dbContext;
        public FreeDaysQueryHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<int> Handle(FreeDaysQuery request, CancellationToken cancellationToken)
        {
            var employee = await dbContext.Employees
                .AsNoTracking()
                .Include(e => e.VacationPackage)
                .Include(e => e.Vacations)
                .FirstOrDefaultAsync(e => e.Id == request.EmployeeId, cancellationToken) ?? throw new KeyNotFoundException();

            var freeDays = CountFreeDaysForEmployee(employee, employee.Vacations.ToList(), employee.VacationPackage);

            return freeDays;
        }
    }

    private static int CountFreeDaysForEmployee(Employee employee, List<Vacation> vacations, VacationPackage vacationPackage)
    {
        var year = vacationPackage.Year;
        var today = DateTime.Today;

        var usedHours = vacations
            .Where(v => v.EmployeeId == employee.Id 
                   && v.DateUntil.Year == year && v.DateUntil < today)
            .Sum(v => v.NumberOfHours);

        var usedDays = usedHours / 8;

        var remainingDays = vacationPackage.GrantedDays - usedDays;

        return remainingDays > 0 ? remainingDays : 0;
    }
}