namespace EmploRecrutingProject.Domain.Entities;

public class EmployeeHierarchy
{
    private EmployeeHierarchy() 
    {
        // For ORM
    }

    public EmployeeHierarchy(Guid employeeId, Guid superiorId, int relationLevel)
    {
        EmployeeId = employeeId;
        SuperiorId = superiorId;
        RelationLevel = relationLevel;
    }

    public Guid EmployeeId { get; private set; }
    public Guid SuperiorId { get; private set; }
    public int RelationLevel { get; private set; }

    public Employee Employee { get; private set; }
    public Employee Superior { get; private set; }

    public static EmployeeHierarchy Create(Guid employeeId, Guid superiorId, int level)
        => new EmployeeHierarchy(employeeId, superiorId, level);
}
