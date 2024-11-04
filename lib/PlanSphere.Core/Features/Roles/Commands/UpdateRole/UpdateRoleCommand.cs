using System.Text.Json.Serialization;
using MediatR;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.Requests;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Roles.Commands.UpdateRole;

public record UpdateRoleCommand(ulong RoleId, ulong UserId, RoleRequest RoleRequest) : IRequest, ISourceLevelRequest
{
    [JsonIgnore]
    public SourceLevel SourceLevel { get; set; }
    [JsonIgnore]
    public ulong SourceLevelId { get; set; }
}
