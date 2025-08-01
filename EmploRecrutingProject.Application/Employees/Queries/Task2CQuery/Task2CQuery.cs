using EmploRecrutingProject.Application.Abstractions;
using EmploRecrutingProject.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Application.Employees.Queries.Task2CQuery;

public class Task2CQuery : IRequest<List<TeamVm>>
{
    public class Task2CQueryHandler : IRequestHandler<Task2CQuery, List<TeamVm>>
    {
        private readonly IApplicationDbContext dbContext;
        public Task2CQueryHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<TeamVm>> Handle(Task2CQuery request, CancellationToken cancellationToken)
        {
            var result = await dbContext.Teams
                .AsNoTracking()
                .Where(t => t.Employees.All(e => 
                    e.Vacations.All(v => 
                        v.DateSince.Year != 2019 &&
                        v.DateUntil.Year != 2019)))
                .Select(TeamVm.GetMapping())
                .ToListAsync(cancellationToken);
            return result;
        }
    }
}