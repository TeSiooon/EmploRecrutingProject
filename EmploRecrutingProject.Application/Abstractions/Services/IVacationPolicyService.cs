using EmploRecrutingProject.Domain.Entities;

namespace EmploRecrutingProject.Application.Abstractions.Services;

public interface IVacationPolicyService
{
    /// <summary>
    /// Return the number of free vacation days for the employee in the current year.
    /// </summary>
    /// <param name="employee"></param>
    /// <param name="vacations"></param>
    /// <param name="vacationPackage"></param>
    /// <returns></returns>
    int CountFreeDaysForEmployee(Employee employee, IEnumerable<Vacation> vacations, VacationPackage vacationPackage);

    /// <summary>
    /// Check if the employee can request a vacation based on the current policy.
    /// </summary>
    /// <param name="employee"></param>
    /// <param name="vacation"></param>
    /// <param name="vacationPackage"></param>
    /// <returns></returns>
    bool IfEmployeeCanRequestVacation(Employee employee, IEnumerable<Vacation> vacations, VacationPackage vacationPackage);
}
