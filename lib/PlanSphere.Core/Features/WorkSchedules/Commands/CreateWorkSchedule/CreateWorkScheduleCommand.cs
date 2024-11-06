using Domain.Entities.EmbeddedEntities;
using MediatR;
using Newtonsoft.Json;
using PlanSphere.Core.Features.WorkSchedules.Request;

namespace PlanSphere.Core.Features.WorkSchedules.Commands;

public record CreateWorkScheduleCommand (WorkScheduleRequest Request) : IRequest
{
    [JsonIgnore]
    public ulong OrganisationId { get; set; }
    [JsonIgnore]
    public ulong SourceLevelId { get; set; }
    [JsonIgnore]
    public SourceLevel SourceLevel { get; set; }
}