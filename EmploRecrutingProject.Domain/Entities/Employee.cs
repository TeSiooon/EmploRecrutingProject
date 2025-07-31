namespace EmploRecrutingProject.Domain.Entities;

public class Employee
{
    private readonly List<Vacation> vacations = new();
    private readonly List<EmployeeHierarchy> children = new();
    private readonly List<EmployeeHierarchy> parents = new();
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

    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public Guid? SuperiorId { get; private set; }
    public Employee Superior { get; private set; }

    public Guid? TeamId { get; private set; }
    public Team Team { get; private set; }

    public Guid? VacationPackageId { get; private set; }
    public VacationPackage VacationPackage { get; private set; }

    public IReadOnlyCollection<Vacation> Vacations => vacations.AsReadOnly();

    public IReadOnlyCollection<EmployeeHierarchy> HierarchyChildren => children;
    public IReadOnlyCollection<EmployeeHierarchy> HierarchyParents => parents;


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
