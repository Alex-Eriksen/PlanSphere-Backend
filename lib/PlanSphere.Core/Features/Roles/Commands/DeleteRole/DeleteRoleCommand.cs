using Domain.Entities.EmbeddedEntities;
using MediatR;
using Newtonsoft.Json;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Roles.Commands.DeleteRole;

public record DeleteRoleCommand(ulong UserId, ulong RoleId) : IRequest, ISourceLevelRequest
{    
    [JsonIgnore]
    public SourceLevel SourceLevel { get; set; }
    [JsonIgnore]
    public ulong SourceLevelId { get; set; }
}
