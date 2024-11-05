using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Interfaces;

public interface ISourceLevelRequest
{
    public SourceLevel SourceLevel { get; set; }
    public ulong SourceLevelId { get; set; }
}