using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Users.Commands.PatchUser;
using PlanSphere.Core.Features.Users.Requests;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Commands.UpdateUser;

[HandlerType(HandlerType.SystemApi)]
public class UpdateUserCommandHandler(
    ILogger<UpdateUserCommandHandler> logger,
    IUserRepository userRepository,
    IMapper mapper
) : IRequestHandler<UpdateUserCommand>
{
    private readonly ILogger<UpdateUserCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Update user");
        _logger.LogInformation("Retrieving user with id: [{userId}]", command.UserId);
        var user = await _userRepository.GetByIdAsync(command.UserId, cancellationToken);
        _logger.LogInformation("Retrieved user with id: [{userId}]", command.UserId);

        _logger.LogInformation("Mapping user with id: [{userId}]", command.UserId);
        var mappedUser = _mapper.Map(command.Request, user);
        _logger.LogInformation("Mapped user with id: [{userId}]", command.UserId);

        _logger.LogInformation("Mapping roles with new roles on user with id: [{userId}]", command.UserId);
        var currentRoleIds = mappedUser.Roles.Select(r => r.RoleId).ToList();
        var newRoleIds = command.Request.RoleIds;
        mappedUser.Roles.RemoveAll(r => !newRoleIds.Contains(r.RoleId));
        var rolesToAdd = newRoleIds
            .Except(currentRoleIds)
            .Select(roleId => new UserRole {RoleId = roleId, UserId = mappedUser.Id});
        
        mappedUser.Roles.AddRange(rolesToAdd);
        _logger.LogInformation("Mapped roles with new roles on user with id: [{userId}] with roles: [{roles}]", command.UserId, command.Request.RoleIds);
        
        _logger.LogInformation("Updating user.");
        await _userRepository.UpdateAsync(command.UserId, mappedUser, cancellationToken);
        _logger.LogInformation("Updated user.");
    }
    
}