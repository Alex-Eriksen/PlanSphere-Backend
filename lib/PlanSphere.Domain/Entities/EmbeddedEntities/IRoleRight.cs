namespace Domain.Entities.EmbeddedEntities;

public interface IRoleRight
{
    ulong RoleId { get; set; }
    Role Role { get; set; }
    
    ulong RightId { get; set; }
    Entities.Right Right { get; set; }
}