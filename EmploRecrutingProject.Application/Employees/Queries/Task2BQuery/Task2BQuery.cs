using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Application.Employees.Queries.Task2BQuery;

public class Task2BQuery : IRequest<List<UsedVacationVm>>
{
}
public class Task2BQueryHandler : IRequestHandler<Task2BQuery, List<UsedVacationVm>>
{
    private readonly IVacationRepository vacationRepository;
    public Task2BQueryHandler(IVacationRepository vacationRepository)
    {
        this.vacationRepository = vacationRepository;
    }
    public async Task<List<UsedVacationVm>> Handle(Task2BQuery request, CancellationToken cancellationToken)
    {
        var now = DateTime.Now;
        var currentYear = now.Year;

        var result = await vacationRepository.Query(cancellationToken)
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
