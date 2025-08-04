using EmploRecrutingProject.Application.Abstractions.Services;
using EmploRecrutingProject.Domain.Entities;

namespace EmploRecrutingProject.Infrastructure.Services;

public class VacationPolicyService : IVacationPolicyService
{
    public int CountFreeDaysForEmployee(Employee employee, IEnumerable<Vacation> vacations, VacationPackage vacationPackage)
    {
        var year = vacationPackage.Year;
        var today = DateTime.Today;

        var usedHours = vacations
            .Where(v => v.EmployeeId == employee.Id
                   && v.DateUntil.Year == year && v.DateUntil < today)
            .Sum(v => v.NumberOfHours);

        var usedDays = usedHours / 8;

        var remainingDays = vacationPackage.GrantedDays - usedDays;

        return remainingDays > 0 ? remainingDays : 0;
    }

    public bool IfEmployeeCanRequestVacation(Employee employee, IEnumerable<Vacation> vacations, VacationPackage vacationPackage)
    {
        var freeDays = CountFreeDaysForEmployee(employee, vacations, vacationPackage);
        if(freeDays <= 0)
            return false;

        return true;
    }
}
