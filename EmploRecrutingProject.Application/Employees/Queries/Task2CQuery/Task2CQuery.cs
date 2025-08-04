using EmploRecrutingProject.Application.Abstractions;
using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Application.Employees.Queries.Task2CQuery;

public class Task2CQuery : IRequest<List<TeamVm>>
{
}
public class Task2CQueryHandler : IRequestHandler<Task2CQuery, List<TeamVm>>
{
    private readonly ITeamRepository teamRepository;
    public Task2CQueryHandler(ITeamRepository teamRepository)
    {
        this.teamRepository = teamRepository;
    }
    public async Task<List<TeamVm>> Handle(Task2CQuery request, CancellationToken cancellationToken)
    {
        var result = await teamRepository.Query(cancellationToken)
                .Where(t => t.Employees.All(e =>
                    e.Vacations.All(v =>
                        v.DateSince.Year != 2019 &&
                        v.DateUntil.Year != 2019)))
                .Select(TeamVm.GetMapping())
                .ToListAsync(cancellationToken);

        return result;
    }
}