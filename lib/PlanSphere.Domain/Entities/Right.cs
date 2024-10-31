using Domain.Entities.EmbeddedEntities;
using RightEnum = Domain.Entities.EmbeddedEntities.Right;

namespace Domain.Entities;

public class Right : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }

    public RightEnum AsEnum => Enum.Parse<RightEnum>(Name);
}