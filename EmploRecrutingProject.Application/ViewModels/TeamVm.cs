using EmploRecrutingProject.Application.Common;
using EmploRecrutingProject.Domain.Entities;
using System.Linq.Expressions;

namespace EmploRecrutingProject.Application.ViewModels;

public sealed record TeamVm(Guid Id, string Name) : IViewModel<TeamVm, Team>
{
    private static readonly Func<Team, TeamVm> mapper = GetMapping().Compile();

    public static Expression<Func<Team, TeamVm>> GetMapping()
    {
        return source => new TeamVm(
                source.Id,
                source.Name
            );
    }

    public static TeamVm From(Team source)
    {
        return mapper(source);
    }

    public static TeamVm? FromNullable(Team? source)
    {
        return source is null ? null : mapper(source);
    }
}
