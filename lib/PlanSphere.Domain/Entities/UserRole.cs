namespace Domain.Entities;

public class UserRole
{
    public ulong UserId { get; set; }
    public ulong RoleId { get; set; }
    public Role Role { get; set; }
}