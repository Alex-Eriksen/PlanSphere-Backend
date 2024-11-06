using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Users.Requests;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Commands.PatchUser;

[HandlerType(HandlerType.SystemApi)]
public class PatchUserCommandHandler(
    ILogger<PatchUserCommandHandler> logger,
    IUserRepository userRepository,
    IMapper mapper
) : IRequestHandler<PatchUserCommand>
{
    private readonly ILogger<PatchUserCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(PatchUserCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Patch user");
        _logger.LogInformation("Retrieving user with id: [{userId}]", request.UserId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        _logger.LogInformation("Retrieved user with id: [{userId}]", request.UserId);

        var mappedUser = _mapper.Map<UserPatchRequest>(user);
        request.Request.ApplyTo(mappedUser);
        user = _mapper.Map(mappedUser, user);

        _logger.LogInformation("Updating user.");
        await _userRepository.UpdateAsync(request.UserId, user, cancellationToken);
        _logger.LogInformation("Updated user.");
    }
}