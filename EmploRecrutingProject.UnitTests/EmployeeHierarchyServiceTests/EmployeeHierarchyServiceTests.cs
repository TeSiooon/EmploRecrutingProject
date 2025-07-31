using EmploRecrutingProject.Application.Abstractions.Repositories;
using EmploRecrutingProject.Domain.Entities;
using EmploRecrutingProject.Infrastructure.Services;
using Moq;

namespace EmploRecrutingProject.UnitTests.EmployeeHierarchyServiceTests;

public class EmployeeHierarchyServiceTests
{
    private readonly Mock<IEmployeeRepository> employeeRepositoryMock = new();
    private readonly Mock<IEmployeeHierarchyRepository> employeeHierarchyRepositoryMock = new();
    private readonly EmployeeHierarchyService employeeHierarchyService;

    public EmployeeHierarchyServiceTests()
    {
        employeeHierarchyService = new EmployeeHierarchyService(
            employeeHierarchyRepositoryMock.Object,
            employeeRepositoryMock.Object);
    }
        
    [Fact]
    public async Task RebuildHierarchy_NoSuperior_DeletesOnly()
    {
        // Arrange
        var emp = Employee.Create("Emp", null);

        employeeRepositoryMock.Setup(r => r.GetByIdAsync(emp.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(emp);

        // Act
        await employeeHierarchyService.RebuildHierarchyForEmployeeAsync(emp, CancellationToken.None);

        // Assert
        employeeHierarchyRepositoryMock.Verify(r => r.DeleteByEmployeeIdAsync(emp.Id, It.IsAny<CancellationToken>()), Times.Once);
        employeeHierarchyRepositoryMock.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<EmployeeHierarchy>>(),
            It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task RebuildHierarchy_SingleLevel_AddsOnEntry()
    {
        // Arrange
        var boss = Employee.Create("Boss", null);
        var emp = Employee.Create("Emp", boss.Id);

        employeeRepositoryMock.Setup(r => r.GetByIdAsync(emp.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(emp);

        employeeHierarchyRepositoryMock.Setup(r => r.GetByEmployeeIdAsync(boss.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<EmployeeHierarchy>());

        // Act
        await employeeHierarchyService.RebuildHierarchyForEmployeeAsync(emp, CancellationToken.None);

        // Assert
        employeeHierarchyRepositoryMock.Verify(r => r.DeleteByEmployeeIdAsync(emp.Id, It.IsAny<CancellationToken>()), Times.Once);
        employeeHierarchyRepositoryMock.Verify(r => r.AddRangeAsync(
            It.Is<IEnumerable<EmployeeHierarchy>>(list =>
                    list.Single().EmployeeId == emp.Id
                 && list.Single().SuperiorId == boss.Id
                 && list.Single().RelationLevel == 1
            ),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task RebildHierarchy_MultiLevel_AdssAllLevels()
    {
        // Arrange
        var grand = Employee.Create("Grand", null);
        var boss = Employee.Create("Boss", grand.Id);
        var emp = Employee.Create("Emp", boss.Id);

        employeeRepositoryMock.Setup(r => r.GetByIdAsync(emp.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(emp);

        var ancestors = EmployeeHierarchy.Create(boss.Id, grand.Id, 1);
        employeeHierarchyRepositoryMock.Setup(r => r.GetByEmployeeIdAsync(boss.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<EmployeeHierarchy> { ancestors });

        // Act
        await employeeHierarchyService.RebuildHierarchyForEmployeeAsync(emp, CancellationToken.None);

        // Assert
        employeeHierarchyRepositoryMock.Verify(r => r.DeleteByEmployeeIdAsync(emp.Id, It.IsAny<CancellationToken>()),
            Times.Once);

        employeeHierarchyRepositoryMock.Verify(R => R.AddRangeAsync(
            It.Is<IEnumerable<EmployeeHierarchy>>(list =>
                list.Count() == 2
                && list.Any(eh => eh.SuperiorId == boss.Id && eh.RelationLevel == 1)
                && list.Any(eh => eh.SuperiorId == grand.Id && eh.RelationLevel == 2)
            ),
            It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task GetSuperiorRow_WhenRelationExists_ReturnsRelationLevel()
    {
        // Arrange
        var empId = Guid.NewGuid();
        var supId = Guid.NewGuid();
        var hier = EmployeeHierarchy.Create(empId, supId, 5);

        employeeHierarchyRepositoryMock.Setup(r => r.FindAsync(empId, supId, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(hier);

        // Act
        var level = await employeeHierarchyService.GetSuperiorRowOfEmployeeAsync(empId, supId);

        // Assert
        Assert.Equal(5, level);
    }

    [Fact]
    public async Task GetSuperiorRow_WhenRelationDoesNotExist_ReturnsNull()
    {
        // Arrange
        var empId = Guid.NewGuid();
        var supId = Guid.NewGuid();

        employeeHierarchyRepositoryMock.Setup(r => r.FindAsync(empId, supId, It.IsAny<CancellationToken>()))
                 .ReturnsAsync((EmployeeHierarchy?)null);

        // Act
        var level = await employeeHierarchyService.GetSuperiorRowOfEmployeeAsync(empId, supId);

        // Assert
        Assert.Null(level);
    }

    [Fact]
    public async Task RebuildHierarchy_ShouldHandleCyclicDataWithoutCrash()
    {
        // Arrange: A -> B, B -> A 
        var a = Employee.Create("A", null);
        var b = Employee.Create("B", a.Id);
        a.Update(a.Name, b.Id);

        employeeRepositoryMock.Setup(r => r.GetByIdAsync(a.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(a);

        var cycleEntry = EmployeeHierarchy.Create(a.Id, b.Id, 1);

        employeeHierarchyRepositoryMock.Setup(r => r.GetByEmployeeIdAsync(b.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<EmployeeHierarchy> { cycleEntry });

        employeeHierarchyRepositoryMock.Setup(r => r.GetByEmployeeIdAsync(a.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<EmployeeHierarchy>());

        // Act
        var exception = await Record.ExceptionAsync(() =>
            employeeHierarchyService.RebuildHierarchyForEmployeeAsync(a, CancellationToken.None));

        // Assert
        Assert.Null(exception);

        employeeHierarchyRepositoryMock.Verify(r => r.DeleteByEmployeeIdAsync(a.Id, It.IsAny<CancellationToken>()), Times.Once);
        employeeHierarchyRepositoryMock.Verify(r => r.AddRangeAsync(It.IsAny<IEnumerable<EmployeeHierarchy>>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}
