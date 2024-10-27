namespace Domain.Entities;

public class TeamRole
{
    public ulong TeamId { get; set; }
    public ulong RoleId { get; set; }
    public bool IsInheritanceActive { get; set; }
}