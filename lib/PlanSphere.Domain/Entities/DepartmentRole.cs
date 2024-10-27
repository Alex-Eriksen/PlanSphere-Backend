namespace Domain.Entities;

public class DepartmentRole
{
    public ulong DepartmentId { get; set; }
    public ulong RoleId { get; set; }
    public bool IsInheritanceActive { get; set; }
}