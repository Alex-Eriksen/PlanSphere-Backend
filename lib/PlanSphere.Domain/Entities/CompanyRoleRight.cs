using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class CompanyRoleRight : BaseEntity
{
    public ulong CompanyId { get; set; }
    public virtual Company Company { get; set; }
    
    public ulong RoleId { get; set; }
    public virtual Role Role { get; set; }
    
    public ulong RightId { get; set; }
    public virtual Right Right { get; set; }
}