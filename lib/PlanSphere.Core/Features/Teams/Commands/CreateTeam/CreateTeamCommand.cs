using System.Text.Json.Serialization;
using MediatR;
using PlanSphere.Core.Features.Teams.Request;

namespace PlanSphere.Core.Features.Teams.Commands.CreateTeam;

public record CreateTeamCommand (TeamRequest Request) : IRequest
{
    [JsonIgnore]
    public ulong DepartmentId { get; set; }
    public bool InheritAddress { get; set; }
}