using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Roles.Commands.AssignRole;

[HandlerType(HandlerType.SystemApi)]
public class AssignRoleCommandHandler(
    ILogger<AssignRoleCommandHandler> logger,
    IUserRepository userRepository
) : IRequestHandler<AssignRoleCommand>
{
    private readonly ILogger<AssignRoleCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task Handle(AssignRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Assign role to user.");
        _logger.BeginScope("Assigning role with id: [{roleId}] to user with id: [{userId}]", request.RoleId, request.UserId);
        await _userRepository.AssignRoleAsync(request.UserId, request.RoleId, cancellationToken);
        _logger.BeginScope("Assigned role with id: [{roleId}] to user with id: [{userId}]", request.RoleId, request.UserId);
    }
}