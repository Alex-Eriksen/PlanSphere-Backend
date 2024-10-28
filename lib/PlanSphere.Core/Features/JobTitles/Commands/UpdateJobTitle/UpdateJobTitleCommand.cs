using MediatR;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.JobTitles.Requests;

namespace PlanSphere.Core.Features.JobTitles.Commands.UpdateJobTitle;

public record UpdateJobTitleCommand(SourceLevel SourceLevel, ulong SourceLevelId, JobTitleUpdateRequest Request) : IRequest
{
    public ulong Id { get; set; }
}
