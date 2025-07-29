namespace EmploRecrutingProject.Domain.Entities;

public class VacationPackage
{
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

    public static VacationPackage Create(string name, int grantedDays, int year)
    {
        return new VacationPackage(name, grantedDays, year);
    }
}
