namespace Domain.Entities;

public class OrganisationRole
{
    public ulong OrganisationId { get; set; }
    public virtual Organisation Organisation { get; set; }
    
    public ulong RoleId { get; set; }
    public virtual Role Role { get; set; }
    
    public bool IsInheritanceActive { get; set; }
}