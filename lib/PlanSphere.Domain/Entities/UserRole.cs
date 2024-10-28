namespace Domain.Entities;

public class UserRole
{
    public ulong UserId { get; set; }
    public virtual User User { get; set; }
    public ulong RoleId { get; set; }
    public virtual Role Role { get; set; }
}