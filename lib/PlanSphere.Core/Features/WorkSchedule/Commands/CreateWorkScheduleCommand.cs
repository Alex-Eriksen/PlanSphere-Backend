using Domain.Entities.EmbeddedEntities;
using MediatR;
using Newtonsoft.Json;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.WorkSchedule.Request;

namespace PlanSphere.Core.Features.WorkSchedule.Commands.CreateWorkSchedule;

public record CreateWorkScheduleCommand (WorkScheduleRequest Request) : IRequest
{
    [JsonIgnore]
    public ulong OrganisationId { get; set; }
    [JsonIgnore]
    public ulong SourceLevelId { get; set; }
    [JsonIgnore]
    public SourceLevel SourceLevel { get; set; }
}