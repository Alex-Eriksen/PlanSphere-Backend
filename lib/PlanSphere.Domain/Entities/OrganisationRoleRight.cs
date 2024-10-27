using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class OrganisationRoleRight : BaseEntity
{
    public ulong OrganisationId { get; set; }
    public ulong RoleId { get; set; }
    public ulong RightId { get; set; }
}