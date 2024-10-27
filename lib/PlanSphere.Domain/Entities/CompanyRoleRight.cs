using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class CompanyRoleRight : BaseEntity
{
    public ulong CompanyId { get; set; }
    public ulong RoleId { get; set; }
    public ulong RightId { get; set; }
}