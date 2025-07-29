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

    public Guid Id { get; set; }
    public DateTime DateSince { get; set; }
    public DateTime DateUntil { get; set; }
    public int NumberOfHours { get; set; }
    public int IsPartialVacation { get; set; }
    public Guid EmployeeId { get; private set; }
    public Employee Employee { get; private set; }

    public static Vacation Create(DateTime since, DateTime until, int hours, bool isPartial, Guid employeeId)
    {
        return new Vacation(since, until, hours, isPartial, employeeId);
    }
}
