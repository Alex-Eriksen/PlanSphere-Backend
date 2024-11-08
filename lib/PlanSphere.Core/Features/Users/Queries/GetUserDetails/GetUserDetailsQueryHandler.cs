using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Users.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Queries.GetUserDetails;

[HandlerType(HandlerType.SystemApi)]
public class GetUserDetailsQueryHandler(
    ILogger<GetUserDetailsQueryHandler> logger,
    IMapper mapper,
    IUserRepository userRepository
) : IRequestHandler<GetUserDetailsQuery, UserDTO>
{
    private readonly ILogger<GetUserDetailsQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<UserDTO> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Get user details");
        _logger.LogInformation("Retrieving user with id: [{userId}]", request.UserId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        _logger.LogInformation("Retrieved user with id: [{userId}]", request.UserId);

        var userDto = _mapper.Map<UserDTO>(user);
        
        return userDto;
    }
}