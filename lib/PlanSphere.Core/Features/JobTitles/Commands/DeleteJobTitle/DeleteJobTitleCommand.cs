using MediatR;
using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Features.JobTitles.Commands.DeleteJobTitle;

public record DeleteJobTitleCommand(SourceLevel SourceLevel, ulong SourceLevelId) : IRequest
{
    public ulong Id { get; set; }
}