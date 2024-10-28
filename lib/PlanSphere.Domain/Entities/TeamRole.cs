namespace Domain.Entities;

public class TeamRole
{
    public ulong TeamId { get; set; }
    public virtual Team Team { get; set; }
    
    public ulong RoleId { get; set; }
    public virtual Role Role { get; set; }
    
    public bool IsInheritanceActive { get; set; }
}