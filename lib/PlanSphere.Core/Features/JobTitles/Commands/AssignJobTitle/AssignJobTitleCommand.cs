using MediatR;

namespace PlanSphere.Core.Features.JobTitles.Commands.AssignJobTitle;

public record AssignJobTitleCommand(ulong JobTitleId, ulong UserId) : IRequest;
