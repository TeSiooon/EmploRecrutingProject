using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Domain.Entities;
using EmploRecrutingProject.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Infrastructure.Repositories;

public class VacationRepository : IVacationRepository
{
    private readonly EmploRecrutingProjectDbContext dbContext;
    public VacationRepository(EmploRecrutingProjectDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public IQueryable<Vacation> Query(CancellationToken cancellationToken = default)
    {
        return dbContext.Vacations
            .AsNoTracking();
    }
}
