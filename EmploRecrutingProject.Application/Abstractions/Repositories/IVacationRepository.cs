using EmploRecrutingProject.Domain.Entities;

namespace EmploRecrutingProject.Application.Abstractions.Repositories;

public interface IVacationRepository
{
    IQueryable<Vacation> Query(CancellationToken cancellationToken = default);
}
