using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.DTOs;
using PlanSphere.Core.Interfaces.Repositories;
using Right = Domain.Entities.EmbeddedEntities.Right;

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
        var user = await _userRepository.GetByIdWithRolesAndJobTitlesAsync(request.UserId, cancellationToken);
        var userRoles = user.Roles.Select(x => x.Role).ToList();

        var roles = new List<Role>();
        roles.AddRange(GetRolesFromRoles(userRoles, Right.View, x => x.OrganisationRoleRights, x => x.Organisation.Roles.Select(x => x.Role)));
        roles.AddRange(GetRolesFromRoles(userRoles, Right.View, x => x.OrganisationRoleRights, x => x.Organisation.Companies.SelectMany(c => c.Roles).Select(x => x.Role)));
        roles.AddRange(GetRolesFromRoles(userRoles, Right.View, x => x.OrganisationRoleRights, x => x.Organisation.Companies.SelectMany(c => c.Departments.SelectMany(d => d.Roles).Select(x => x.Role))));
        roles.AddRange(GetRolesFromRoles(userRoles, Right.View, x => x.OrganisationRoleRights, x => x.Organisation.Companies.SelectMany(c => c.Departments.SelectMany(d => d.Teams.SelectMany(t => t.Roles).Select(x => x.Role)))));
        
        roles.AddRange(GetRolesFromRoles(userRoles, Right.View, x => x.CompanyRoleRights, x => x.Company.Departments.SelectMany(d => d.Teams.SelectMany(t => t.Roles).Select(x => x.Role))));
        roles.AddRange(GetRolesFromRoles(userRoles, Right.View, x => x.CompanyRoleRights, x => x.Company.Departments.SelectMany(d => d.Roles).Select(x => x.Role)));
        roles.AddRange(GetRolesFromRoles(userRoles, Right.View, x => x.CompanyRoleRights, x => x.Company.Roles.Select(x => x.Role)));
        
        roles.AddRange(GetRolesFromRoles(userRoles, Right.View, x => x.DepartmentRoleRights, x => x.Department.Roles.Select(x => x.Role)));
        roles.AddRange(GetRolesFromRoles(userRoles, Right.View, x => x.DepartmentRoleRights, x => x.Department.Teams.SelectMany(t => t.Roles).Select(x => x.Role)));
        
        roles.AddRange(GetRolesFromRoles(userRoles, Right.View, x => x.TeamRoleRights, x => x.Team.Roles.Select(x => x.Role)));

        
        _logger.LogInformation("Retrieved roles from repository.");

        roles = roles.Distinct().ToList();
        var roleLookUpDTOs = _mapper.Map<List<RoleLookUpDTO>>(roles);

        return roleLookUpDTOs;
    }
    
    private IEnumerable<Role> GetRolesFromRoles<T>(
        IEnumerable<Role> roles,
        Right accessRight,
        Func<Role, IEnumerable<T>> rightsSelector,
        Func<T, IEnumerable<Role>> rolesSelector
    ) where T : IRoleRight
    {
        return roles
            .SelectMany(rightsSelector)
            .Where(right => right.Right.AsEnum <= accessRight)
            .SelectMany(rolesSelector)
            .ToList();
    }
}