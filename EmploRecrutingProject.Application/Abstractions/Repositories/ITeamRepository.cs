using EmploRecrutingProject.Domain.Entities;

namespace EmploRecrutingProject.Application.Abstractions.Repositories;

public interface ITeamRepository
{
    IQueryable<Team> Query(CancellationToken cancellationToken = default);
}
