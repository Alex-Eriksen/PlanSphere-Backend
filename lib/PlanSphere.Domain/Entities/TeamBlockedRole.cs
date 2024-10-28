namespace Domain.Entities;

public class TeamBlockedRole
{
    public ulong TeamId { get; set; }
    public virtual Team Team { get; set; }
    
    public ulong RoleId { get; set; }
    public virtual Role Role { get; set; }
}