namespace EmploRecrutingProject.Domain.Entities;

public class Team
{
    private readonly List<Employee> employees = new();
    private Team()
    {
        //For ORM
    }

    public Team(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public IReadOnlyCollection<Employee> Employees => employees.AsReadOnly();

    public static Team Create(string name)
    {
        return new Team(name);
    }
}
