using EmploRecrutingProject.Application.Abstractions;
using EmploRecrutingProject.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Application.Employees.Queries.Task2BQuery;

public class Task2BQuery : IRequest<List<UsedVacationVm>>
{
}
public class Task2BQueryHandler : IRequestHandler<Task2BQuery, List<UsedVacationVm>>
{
    private readonly IApplicationDbContext dbContext;
    public Task2BQueryHandler(IApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public async Task<List<UsedVacationVm>> Handle(Task2BQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var currentYear = now.Year;

        var result = await dbContext.Vacations
            .AsNoTracking()
            .Include(v => v.Employee)
            .Where(v =>
                v.DateSince.Year == currentYear &&
                v.DateUntil.Year == currentYear &&
                v.DateUntil < now)
            .Select(UsedVacationVm.GetMapping())
            .ToListAsync(cancellationToken);

        return result;
    }
}
