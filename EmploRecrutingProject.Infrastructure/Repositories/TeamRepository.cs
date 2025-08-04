using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Domain.Entities;
using EmploRecrutingProject.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
    private readonly EmploRecrutingProjectDbContext dbContext;

    public TeamRepository(EmploRecrutingProjectDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public IQueryable<Team> Query(CancellationToken cancellationToken = default)
    {
        return dbContext.Teams
            .AsNoTracking();
    }
}
