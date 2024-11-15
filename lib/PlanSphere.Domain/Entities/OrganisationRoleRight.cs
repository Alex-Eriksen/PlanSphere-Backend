using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class OrganisationRoleRight : BaseEntity, IRoleRight
{
    public ulong OrganisationId { get; set; }
    public virtual Organisation Organisation { get; set; }
    
    public ulong RoleId { get; set; }
    public virtual Role Role { get; set; }
    
    public ulong RightId { get; set; }
    public virtual Right Right { get; set; }
}