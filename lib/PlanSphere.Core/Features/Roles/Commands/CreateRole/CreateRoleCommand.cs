using System.Text.Json.Serialization;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.Requests;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Roles.Commands.CreateRole;

public record CreateRoleCommand(RoleRequest Request, ulong UserId) : IRequest, ISourceLevelRequest
{
    [JsonIgnore]
    public SourceLevel SourceLevel { get; set; }
    
    [JsonIgnore]
    public ulong SourceLevelId { get; set; }
}