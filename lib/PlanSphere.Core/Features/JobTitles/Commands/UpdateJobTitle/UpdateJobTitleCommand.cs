using Domain.Entities.EmbeddedEntities;
using MediatR;
using Newtonsoft.Json;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.JobTitles.Requests;

namespace PlanSphere.Core.Features.JobTitles.Commands.UpdateJobTitle;

public record UpdateJobTitleCommand(JobTitleRequest Request) : IRequest
{
    [JsonIgnore]
    public ulong SourceLevelId { get; set; }
    [JsonIgnore]
    public SourceLevel SourceLevel { get; set; }
    public ulong Id { get; set; }
}