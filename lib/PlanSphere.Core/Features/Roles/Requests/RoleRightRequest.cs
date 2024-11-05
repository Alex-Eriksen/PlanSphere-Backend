using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Features.Roles.Requests;

public class RoleRightRequest
{
    public SourceLevel SourceLevel { get; set; }
    
    /// <summary>
    /// Corrosponds to the source level, so if the source level is organisation it will be an organisation id and so on.
    /// </summary>
    public ulong SourceLevelId { get; set; }
    
    public ulong RightId { get; set; }
}