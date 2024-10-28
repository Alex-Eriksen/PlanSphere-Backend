using Domain.Entities.EmbeddedEntities;

namespace Domain.Entities;

public class Right : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
}