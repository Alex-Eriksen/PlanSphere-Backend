namespace Domain.Entities;

public class TeamRoleRight
{
    public ulong TeamId { get; set; }
    public virtual Team Team { get; set; }
    
    public ulong RoleId { get; set; }
    public virtual Role Role { get; set; }
    
    public ulong RightId { get; set; }
    public virtual Right Right { get; set; }
}