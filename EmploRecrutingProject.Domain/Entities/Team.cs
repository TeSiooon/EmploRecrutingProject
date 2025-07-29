namespace EmploRecrutingProject.Domain.Entities;

public class Team
{
    private Team()
    {
        //For ORM
    }

    public Team(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }

    public static Team Create(string name)
    {
        return new Team(name);
    }
}
