using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.JobTitles.Commands.ToggleJobTitleInheritance;

public record ToggleJobTitleInheritanceCommand(ulong JobTitleId) : IRequest<bool>, ISourceLevelRequest
{
    public ulong SourceLevelId { get; set; }
    public SourceLevel SourceLevel { get; set; }
}
