using AutoMapper;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Roles.Queries.LookUpRoles;

[HandlerType(HandlerType.SystemApi)]
public class LookUpRolesQueryHandler(
    IUserRepository userRepository,
    ILogger<LookUpRolesQueryHandler> logger,
    IMapper mapper
) : IRequestHandler<LookUpRolesQuery, List<RoleLookUpDTO>>
{
    private readonly ILogger<LookUpRolesQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<RoleLookUpDTO>> Handle(LookUpRolesQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Looking up roles.");
        _logger.LogInformation("Retrieving roles from repository.");
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        var userRoles = user.Roles.Select(x => x.Role).ToList();

        var roles = userRoles
            .SelectMany(x => x.OrganisationRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Organisation)
            .SelectMany(x => x.Roles)
            .Select(x => x.Role)
            .ToList();
        
        roles.AddRange(userRoles
            .SelectMany(x => x.CompanyRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Company)
            .SelectMany(x => x.Roles)
            .Select(x => x.Role)
            .ToList()
        );
        
        roles.AddRange(userRoles
            .SelectMany(x => x.DepartmentRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Department)
            .SelectMany(x => x.Roles)
            .Select(x => x.Role)
            .ToList()
        );
        
        roles.AddRange(userRoles
            .SelectMany(x => x.TeamRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Team)
            .SelectMany(x => x.Roles)
            .Select(x => x.Role)
            .ToList()
        );
        
        _logger.LogInformation("Retrieved roles from repository.");

        roles = roles.Distinct().ToList();
        var roleLookUpDTOs = _mapper.Map<List<RoleLookUpDTO>>(roles);

        return roleLookUpDTOs;
    }
}