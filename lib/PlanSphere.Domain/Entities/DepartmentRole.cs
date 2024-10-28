namespace Domain.Entities;

public class DepartmentRole
{
    public ulong DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    
    public ulong RoleId { get; set; }
    public virtual Role Role { get; set; }
    
    public bool IsInheritanceActive { get; set; }
}