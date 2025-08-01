using EmploRecrutingProject.Application.Abstractions;
using EmploRecrutingProject.Application.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Application.Employees.Queries.Task2AQuery;

public class GetTask2AQuery : IRequest<List<EmployeeVm>>
{
    public class GetTask2AQueryHandler: IRequestHandler<GetTask2AQuery, List<EmployeeVm>>
    {
        private readonly IApplicationDbContext dbContext;    
        public GetTask2AQueryHandler(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<EmployeeVm>> Handle(GetTask2AQuery request, CancellationToken cancellationToken)
        {
            var result = await dbContext.Employees
                .AsNoTracking()
                .Where(e =>
                    e.Team != null &&
                    e.Team.Name == ".NET" &&
                    e.Vacations.Any(v => v.DateSince.Year == 2019 || v.DateUntil.Year == 2019))
                .Select(EmployeeVm.GetMapping())
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
