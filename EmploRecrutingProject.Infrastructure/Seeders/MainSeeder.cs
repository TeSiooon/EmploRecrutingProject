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
        var teamD = Team.Create("Team D");
        // teamB, teamC, teamD zespoły bez urlopów w 2019

        var pkg2019 = VacationPackage.Create("Standard 2019", 20, 2019);
        var pkg2025 = VacationPackage.Create("Standard 2025", 20, 2025);

        // pracownicy z .Net + urlopy 2019
        var emp1 = Employee.Create("Jan Kowalski");
        emp1.AssignToTeam(teamNet);
        emp1.AssignVacationPackage(pkg2019);

        var emp2 = Employee.Create("Adam Nowak");
        emp2.AssignToTeam(teamNet);
        emp2.AssignVacationPackage(pkg2019);

        var emp3 = Employee.Create("Ewa Wiśniewska");
        emp3.AssignToTeam(teamNet);
        emp3.AssignVacationPackage(pkg2019);

        // urlopy 2019 dla emp1–3
        var vac1 = Vacation.Create(new DateTime(2019, 06, 01), new DateTime(2019, 06, 10), 10 * 8, false, emp1.Id);
        var vac2 = Vacation.Create(new DateTime(2019, 07, 05), new DateTime(2019, 07, 15), 11 * 8, false, emp2.Id);
        var vac3 = Vacation.Create(new DateTime(2019, 08, 10), new DateTime(2019, 08, 20), 10 * 8, false, emp3.Id);

        emp1.AddVacation(vac1);
        emp2.AddVacation(vac2);
        emp3.AddVacation(vac3);

        //pracownicy z urlopami 2025
        var emp4 = Employee.Create("Marta Kowalska");
        emp4.AssignToTeam(teamB);
        emp4.AssignVacationPackage(pkg2025);

        var emp5 = Employee.Create("Tomasz Zieliński");
        emp5.AssignToTeam(teamC);
        emp5.AssignVacationPackage(pkg2025);

        var emp6 = Employee.Create("Katarzyna Mazur");
        emp6.AssignToTeam(teamD);
        emp6.AssignVacationPackage(pkg2025);

        // urlopy 2025
        var vac4 = Vacation.Create(new DateTime(2025, 01, 02), new DateTime(2025, 01, 07), 5 * 8, false, emp4.Id);
        var vac5 = Vacation.Create(new DateTime(2025, 02, 10), new DateTime(2025, 02, 12), 3 * 8, false, emp5.Id);
        var vac6 = Vacation.Create(new DateTime(2025, 03, 15), new DateTime(2025, 03, 20), 5 * 8, false, emp6.Id);

        emp4.AddVacation(vac4);
        emp5.AddVacation(vac5);
        emp6.AddVacation(vac6);

        dbContext.Teams.AddRange(teamNet, teamB, teamC, teamD);
        dbContext.VacationPackages.AddRange(pkg2019, pkg2025);
        dbContext.Employees.AddRange(emp1, emp2, emp3, emp4, emp5, emp6);
        dbContext.Vacations.AddRange(vac1, vac2, vac3, vac4, vac5, vac6);

        await dbContext.SaveChangesAsync();
    }
}
