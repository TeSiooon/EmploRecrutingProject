using EmploRecrutingProject.Application.Common;
using EmploRecrutingProject.Domain.Entities;
using System.Linq.Expressions;

namespace EmploRecrutingProject.Application.ViewModels;

public sealed record EmployeeVm(Guid Id, string Name)  : IViewModel<EmployeeVm, Employee>
{
    private static readonly Func<Employee, EmployeeVm> mapper = GetMapping().Compile();

    public static Expression<Func<Employee, EmployeeVm>> GetMapping()
    {
        return source => new EmployeeVm(
                source.Id,
                source.Name
            );
    }

    public static EmployeeVm From(Employee source)
    {
        return mapper(source);
    }

    public static EmployeeVm? FromNullable(Employee? source)
    {
        return source is null ? null : mapper(source);
    }
}
