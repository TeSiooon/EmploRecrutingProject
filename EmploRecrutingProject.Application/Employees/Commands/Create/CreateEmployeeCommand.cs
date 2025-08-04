using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Application.Abstractions.Services;
using EmploRecrutingProject.Application.Common;
using EmploRecrutingProject.Domain.Entities;
using MediatR;

namespace EmploRecrutingProject.Application.Employees.Commands.Create;

public record CreateEmployeeCommand : IRequest<Guid>
{
    public string Name { get; init; } = default!;
    public Guid? SuperiorId { get; init; }
}
public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
{
    private readonly IEmployeeRepository employeeRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IEmployeeHierarchyService employeeHierarchyService;
    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork
        , IEmployeeHierarchyService employeeHierarchyService)
    {
        this.employeeRepository = employeeRepository;
        this.unitOfWork = unitOfWork;
        this.employeeHierarchyService = employeeHierarchyService;
    }
    public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = Employee.Create(request.Name, request.SuperiorId);

        await employeeRepository.Create(employee, cancellationToken);

        await employeeHierarchyService.RebuildHierarchyForEmployeeAsync(employee, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }
}
