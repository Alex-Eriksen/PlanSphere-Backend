using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Users.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Queries.GetLoggedInUser;

[HandlerType(HandlerType.SystemApi)]
public class GetLoggedInUserQueryHandler(
    ILogger<GetLoggedInUserQueryHandler> logger,
    IUserRepository userRepository,
    IMapper mapper
) : IRequestHandler<GetLoggedInUserQuery, LoggedInUserDTO>
{
    private readonly ILogger<GetLoggedInUserQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<LoggedInUserDTO> Handle(GetLoggedInUserQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Get logged in user information.");

        _logger.LogInformation("Retrieving user data from refresh token.");
        var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken, cancellationToken);
        _logger.LogInformation("Retrieved user data from refresh token.");

        if (request.ClaimedUserId != user.Id)
        {
            _logger.LogInformation("You are not authorized to view this data!");
            throw new UnauthorizedAccessException(ErrorMessageConstants.UnauthorizedActionMessage);
        }
        
        var loggedInUserDto = _mapper.Map<LoggedInUserDTO>(user);

        return loggedInUserDto;
    }
}