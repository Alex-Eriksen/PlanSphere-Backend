using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Commands.DeleteUser;

[HandlerType(HandlerType.SystemApi)]
public class DeleteUserCommandHandler(
    IUserRepository userRepository,
    ILogger<DeleteUserCommandHandler> logger
) : IRequestHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly ILogger<DeleteUserCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Starting to delete user with [DeleteUserCommandHandler] with id: [{userId}]",request.UserId);
        _logger.LogInformation("Deleting user with id: [{id}]", request.UserId);
        await _userRepository.DeleteAsync(request.UserId, cancellationToken);
        _logger.LogInformation("Deleted user with id: [{id}]", request.UserId);
    }
}