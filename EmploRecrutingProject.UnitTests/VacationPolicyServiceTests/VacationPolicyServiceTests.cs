using EmploRecrutingProject.Domain.Entities;
using EmploRecrutingProject.Infrastructure.Services;

namespace EmploRecrutingProject.UnitTests.VacationPolicyServiceTests;

public class VacationPolicyServiceTests
{
    private readonly VacationPolicyService service = new VacationPolicyService();

    [Fact]
    public void Employee_Can_Request_Vacation()
    {
        // Arrange
        var employee = Employee.Create("Test user", null);
        var grantedDays = 5;
        var year = DateTime.Today.Year;
        var vacationPackage = VacationPackage.Create("PKG", grantedDays, year);

        var yesterday = DateTime.Today.AddDays(-1);
        var vac = Vacation.Create(yesterday, yesterday, 8 * 2, false, employee.Id);
        var vacations = new List<Vacation> { vac };

        // Act
        bool canRequest = service.IfEmployeeCanRequestVacation(employee, vacations, vacationPackage);

        // Assert
        Assert.True(canRequest);
    }

    [Fact]
    public void Employee_Cannot_Request_Vacation()
    {
        // Arrange
        var employee = Employee.Create("Test User");
        int grantedDays = 3;
        var year = DateTime.Today.Year;
        var vacationPackage = VacationPackage.Create("PKG", grantedDays, year);

        var start = DateTime.Today.AddDays(-3);
        var end = DateTime.Today.AddDays(-1);
                                             
        var fullUse = Vacation.Create(start, end, 8 * 3, false, employee.Id);
        var vacations = new List<Vacation> { fullUse };

        // Act
        bool canRequest =service.IfEmployeeCanRequestVacation(employee, vacations, vacationPackage);

        // Assert
        Assert.False(canRequest);
    }
}
