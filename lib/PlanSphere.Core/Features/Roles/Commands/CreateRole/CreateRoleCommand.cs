using System.Text.Json.Serialization;
using MediatR;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.Requests;

namespace PlanSphere.Core.Features.Roles.Commands.CreateRole;

public record CreateRoleCommand(RoleRequest Request) : IRequest
{
    [JsonIgnore]
    public SourceLevel SourceLevel { get; set; }
    
    [JsonIgnore]
    public ulong SourceLevelId { get; set; }
    
    [JsonIgnore]
    public ulong UserId { get; set; }
}