namespace Domain.Entities;

public class DepartmentRoleRight
{
    public ulong DepartmentId { get; set; }
    public virtual Department Department { get; set; }
    
    public ulong RoleId { get; set; }
    public virtual Role Role { get; set; }
    
    public ulong RightId { get; set; }
    public virtual Right Right { get; set; }
}