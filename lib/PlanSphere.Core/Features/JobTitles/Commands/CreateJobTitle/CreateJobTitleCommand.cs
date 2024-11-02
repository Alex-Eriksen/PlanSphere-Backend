using MediatR;
using Newtonsoft.Json;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.JobTitles.Requests;

namespace PlanSphere.Core.Features.JobTitles.Commands.CreateJobTitle;

public record CreateJobTitleCommand(JobTitleRequest Request) : IRequest
{
    [JsonIgnore]
    public ulong SourceLevelId { get; set; }
    [JsonIgnore]
    public SourceLevel SourceLevel { get; set; }
    
}