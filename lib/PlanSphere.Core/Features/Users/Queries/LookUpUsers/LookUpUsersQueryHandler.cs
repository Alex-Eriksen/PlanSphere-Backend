using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Users.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Queries.LookUpUsers;

[HandlerType(HandlerType.SystemApi)]
public class LookUpUsersQueryHandler(
    ILogger<LookUpUsersQueryHandler> logger,
    IUserRepository userRepository,
    IMapper mapper
) : IRequestHandler<LookUpUsersQuery, List<UserLookUpDTO>>
{
    private readonly ILogger<LookUpUsersQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<UserLookUpDTO>> Handle(LookUpUsersQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Look up users.");
        _logger.LogInformation("Retrieving user from organisation with id: [{organisationId}]", request.OrganisationId);
        var users = await _userRepository.GetQueryable().Where(x => x.OrganisationId == request.OrganisationId).ToListAsync(cancellationToken);
        _logger.LogInformation("Retrieved user from organisation with id: [{organisationId}]", request.OrganisationId);

        var userLookUpDtos = _mapper.Map<List<UserLookUpDTO>>(users);

        return userLookUpDtos;
    }
}