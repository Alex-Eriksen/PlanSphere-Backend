using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Users.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Queries.GetUser;

[HandlerType(HandlerType.SystemApi)]
public class GetUserQueryHandler (
    IUserRepository userRepository,
    ILogger<GetUserQueryHandler> logger,
    IMapper mapper
) : IRequestHandler<GetUserQuery, UserListDTO>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly ILogger<GetUserQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<UserListDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Starting to Get user with [GetUserQueryHandler]");
        _logger.LogInformation("Fetching user with id: [{UserId}]", request.UserId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        _logger.LogInformation("Fetched user with id: [{userId}]", request.UserId);
        
        _logger.LogInformation("Mapping user with id: [{userId}]", request.UserId);
        var mappedUser = _mapper.Map<UserListDTO>(user);

        return mappedUser;
    }
}