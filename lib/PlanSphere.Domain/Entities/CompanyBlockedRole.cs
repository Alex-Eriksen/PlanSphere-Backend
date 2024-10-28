namespace Domain.Entities;

public class CompanyBlockedRole
{
    public ulong CompanyId { get; set; }
    public virtual Company Company { get; set; }
    
    public ulong RoleId { get; set; }
    public virtual Role Role { get; set; }
}