using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Roles.Commands.DeleteRole;

[HandlerType(HandlerType.SystemApi)]
public class DeleteRoleCommandHandler(
    ILogger<DeleteRoleCommandHandler> logger,
    IRoleRepository roleRepository
) : IRequestHandler<DeleteRoleCommand>
{
    private readonly ILogger<DeleteRoleCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Delete role.");
        _logger.LogInformation("Deleting role with id: [{roleId}] by user with id: [{userId}]", request.RoleId, request.UserId);
        await _roleRepository.DeleteAsync(request.RoleId, cancellationToken);
        _logger.LogInformation("Deleted role with id: [{roleId}] by user with id: [{userId}]", request.RoleId, request.UserId);
    }
}