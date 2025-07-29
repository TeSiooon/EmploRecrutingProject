namespace EmploRecrutingProject.Domain.Entities;

public class VacationPackage
{
    private readonly List<Employee> employees = new();
    private VacationPackage()
    {
        //For ORM
    }

    public VacationPackage(string name, int grantedDays, int year)
    {
        Id = Guid.NewGuid();
        Name = name;
        GrantedDays = grantedDays;
        Year = year;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public int GrantedDays { get; set; }
    public int Year { get; set; }
    public IReadOnlyCollection<Employee> Employees => employees.AsReadOnly();

    public static VacationPackage Create(string name, int grantedDays, int year)
    {
        return new VacationPackage(name, grantedDays, year);
    }
}
