using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Features.Roles.DTOs;

public class RightDTO : BaseDTO
{
    public ulong RightId { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    
    public SourceLevel SourceLevel { get; set; }
    public ulong SourceLevelId { get; set; }
}