namespace EmploRecrutingProject.Domain.Entities;

public class Employee
{
    private Employee()
    {
        //For ORM
    }

    public Employee(string name, Guid? superiorId)
    {
        Id = Guid.NewGuid();
        Name = name;
        SuperiorId = superiorId;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? SuperiorId { get; set; }
    public Employee Superior { get; set; }

    public static Employee Create(string name, Guid? superiorId = null)
    {
        return new Employee(name, superiorId);
    }

    public void Update(string name, Guid? superiorId = null)
    {
        Name = name;
        SuperiorId = superiorId;
    }
}
