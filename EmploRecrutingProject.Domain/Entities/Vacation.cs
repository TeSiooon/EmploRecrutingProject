namespace EmploRecrutingProject.Domain.Entities;

public class Vacation
{
    private Vacation()
    {
        //For ORM
    }

    public Vacation(DateTime since, DateTime until, int hours, bool isPartial, Guid employeeId)
    {
        Id = Guid.NewGuid();
        DateSince = since;
        DateUntil = until;
        NumberOfHours = hours;
        IsPartialVacation = isPartial ? 1 : 0;
        EmployeeId = employeeId;
    }

    public Guid Id { get; private set; }
    public DateTime DateSince { get; private set; }
    public DateTime DateUntil { get; private set; }
    public int NumberOfHours { get; private set; }
    public int IsPartialVacation { get; private set; }
    public Guid EmployeeId { get; private set; }
    public Employee Employee { get; private set; }

    public static Vacation Create(DateTime since, DateTime until, int hours, bool isPartial, Guid employeeId)
    {
        return new Vacation(since, until, hours, isPartial, employeeId);
    }
}
