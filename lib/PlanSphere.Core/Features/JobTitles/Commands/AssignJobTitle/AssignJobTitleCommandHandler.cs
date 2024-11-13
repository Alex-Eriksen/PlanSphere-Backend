using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.JobTitles.Commands.AssignJobTitle;

[HandlerType(HandlerType.SystemApi)]
public class AssignJobTitleCommandHandler(
    ILogger<AssignJobTitleCommandHandler> logger,
    IUserRepository userRepository
) : IRequestHandler<AssignJobTitleCommand>
{
    private readonly ILogger<AssignJobTitleCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task Handle(AssignJobTitleCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Assign job title to user.");
        _logger.LogInformation("Assigning job title with id: [{jobTitleId}] to user with id: [{userId}]", request.JobTitleId, request.UserId);
        await _userRepository.AssignJobTitleAsync(request.UserId, request.JobTitleId, cancellationToken);
        _logger.LogInformation("Assigned job title with id: [{jobTitleId}] to user with id: [{userId}]", request.JobTitleId, request.UserId);
    }
}