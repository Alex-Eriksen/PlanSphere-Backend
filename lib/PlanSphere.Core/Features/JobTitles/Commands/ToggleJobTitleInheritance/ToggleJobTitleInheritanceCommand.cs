using MediatR;

namespace PlanSphere.Core.Features.JobTitles.Commands.ToggleJobTitleInheritance;

public record ToggleJobTitleInheritanceCommand(ulong JobTitleId) : IRequest<bool>;
