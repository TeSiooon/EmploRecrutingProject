using EmploRecrutingProject.Application.Employees.Commands.Create;
using FluentAssertions;
using Xunit;

namespace EmploRecrutingProject.IntegrationTests.Employees.Create;

[Collection("EmploRecrutingProject Collection")]

public class CreateEmployeeCommandTests
{
    private readonly EmploRecrutingProjectFixture fixture;
    public CreateEmployeeCommandTests(EmploRecrutingProjectFixture fixture)
    {
        this.fixture = fixture;
    }

    [Fact]
    public async Task Should_Calculate_Hierarchy_Levels_CorrectlyAsync()
    {
        // Arrange & Act
        var janId = await fixture.ExecuteCommandAsync(new CreateEmployeeCommand
        {
            Name = "Jan Kowalski",
            SuperiorId = null
        });

        var kamilId = await fixture.ExecuteCommandAsync(new CreateEmployeeCommand
        {
            Name = "Kamil Nowak",
            SuperiorId = janId
        });

        var andrzejId = await fixture.ExecuteCommandAsync(new CreateEmployeeCommand
        {
            Name = "Andrzej Wiśniewski",
            SuperiorId = kamilId
        });

        // Assert
        // Kamil -> Jan = 1
        var level1 = await fixture.EmployeeHierarchyService.GetSuperiorRowOfEmployeeAsync(kamilId, janId);
        level1.Should().Be(1);

        // Andrzej -> Kamil = 1
        var levelAndrzejKamil = await fixture.EmployeeHierarchyService.GetSuperiorRowOfEmployeeAsync(andrzejId, kamilId);
        levelAndrzejKamil.Should().Be(1);

        // Andrzej -> Jan = 2
        var levelAndrzejJan = await fixture.EmployeeHierarchyService.GetSuperiorRowOfEmployeeAsync(andrzejId, janId);
        levelAndrzejJan.Should().Be(2);

        // Jan -> Andrzej = null (no direct relation)
        var levelJanAndrzej = await fixture.EmployeeHierarchyService.GetSuperiorRowOfEmployeeAsync(janId, andrzejId);
        levelJanAndrzej.Should().BeNull();
    }
}
