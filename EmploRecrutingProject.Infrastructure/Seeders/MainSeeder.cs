using EmploRecrutingProject.Domain.Entities;
using EmploRecrutingProject.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EmploRecrutingProject.Infrastructure.Seeders;

public class MainSeeder
{
    private readonly EmploRecrutingProjectDbContext dbContext;

    public MainSeeder(EmploRecrutingProjectDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
            await dbContext.Database.MigrateAsync();
        if (!await dbContext.Database.CanConnectAsync())
            return;

        if (await dbContext.Employees.AnyAsync()) return;

        var teamNet = Team.Create(".Net");
        var teamB = Team.Create("Team B");
        var teamC = Team.Create("Team C");

        var vacationPackage2019 = VacationPackage.Create("Standard 2019", 20, 2019);
        var vacationPackage2020 = VacationPackage.Create("Standard 2020", 20, 2020);

        var emp1 = Employee.Create("Jan Kowalski");
        emp1.AssignToTeam(teamNet);
        emp1.AssignVacationPackage(vacationPackage2019);

        var emp2 = Employee.Create("Anna Nowak");
        emp2.AssignToTeam(teamB);
        emp2.AssignVacationPackage(vacationPackage2019);

        var emp3 = Employee.Create("Piotr Zieliński");
        emp3.AssignToTeam(teamC);
        emp3.AssignVacationPackage(vacationPackage2019);

        var emp4 = Employee.Create("Marta Wiśniewska");
        emp4.AssignToTeam(teamNet);
        emp4.AssignVacationPackage(vacationPackage2020);

        var emp5 = Employee.Create("Tomasz Lewandowski");
        emp5.AssignToTeam(teamB);
        emp5.AssignVacationPackage(vacationPackage2020);

        var emp6 = Employee.Create("Katarzyna Wójcik");
        emp6.AssignToTeam(teamC);
        emp6.AssignVacationPackage(vacationPackage2020);

        var vacation1 = Vacation.Create(
            new DateTime(2019, 06, 01),
            new DateTime(2019, 06, 10),
            10 * 8,
            false,
            emp1.Id);

        var vacation2 = Vacation.Create(
            new DateTime(2019, 07, 01),
            new DateTime(2019, 07, 15),
            15 * 8,
            false,
            emp2.Id);

        var vacation3 = Vacation.Create(
            new DateTime(2019, 08, 01),
            new DateTime(2019, 08, 20),
            20 * 8,
            false,
            emp3.Id);

        emp1.AddVacation(vacation1);
        emp2.AddVacation(vacation2);
        emp3.AddVacation(vacation3);

        dbContext.Teams.AddRange(teamNet, teamB, teamC);
        dbContext.VacationPackages.AddRange(vacationPackage2019, vacationPackage2020);
        dbContext.Employees.AddRange(emp1, emp2, emp3, emp4, emp5, emp6);
        dbContext.Vacations.AddRange(vacation1, vacation2, vacation3);

        dbContext.SaveChanges();
    }
}
