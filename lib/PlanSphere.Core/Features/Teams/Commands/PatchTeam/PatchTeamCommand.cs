using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using PlanSphere.Core.Features.Teams.Request;

namespace PlanSphere.Core.Features.Teams.Commands.PatchTeam;

public record PatchTeamCommand(JsonPatchDocument<TeamPatchRequest> PatchDocument) : IRequest
{
    [JsonIgnore] 
    public ulong TeamId { get; set; }
}