using EmploRecrutingProject.Application.Common;
using EmploRecrutingProject.Domain.Entities;
using System.Linq.Expressions;

namespace EmploRecrutingProject.Application.ViewModels;

public sealed record UsedVacationVm(Guid EmployeeId, string Name, double UsedDays) : IViewModel<UsedVacationVm, Vacation>
{
    private static readonly Func<Vacation, UsedVacationVm> mapper = GetMapping().Compile();

    public static Expression<Func<Vacation, UsedVacationVm>> GetMapping()
    {
        return source => new UsedVacationVm(
                source.EmployeeId,
                source.Employee.Name,
                (source.DateUntil - source.DateSince).TotalDays * (source.IsPartialVacation == 1 ? 0.5 : 1)
            );
    }

    public static UsedVacationVm From(Vacation source)
    {
        return mapper(source);
    }

    public static UsedVacationVm? FromNullable(Vacation? source)
    {
        return source is null ? null : mapper(source);
    }
}
