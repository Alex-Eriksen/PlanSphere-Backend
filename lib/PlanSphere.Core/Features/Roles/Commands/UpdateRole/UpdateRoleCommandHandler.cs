using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.Requests;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Roles.Commands.UpdateRole;

[HandlerType(HandlerType.SystemApi)]
public class UpdateRoleCommandHandler(
    ILogger<UpdateRoleCommandHandler> logger,
    IMapper mapper,
    IRoleRepository roleRepository
) : IRequestHandler<UpdateRoleCommand>
{
    private readonly ILogger<UpdateRoleCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Update role");
        _logger.LogInformation("Updating role with id: [{roleId}] by user with id: [{userId}]", request.RoleId, request.UserId);
        var roleRequest = _mapper.Map<Role>(request);
        
        foreach (var roleRightRequest in request.Request.Rights)
        {
            HandleRoleRightSourceLevel(roleRightRequest, roleRequest);
        }

        var role = await _roleRepository.GetByIdAsync(request.RoleId, cancellationToken);

        role.Name = roleRequest.Name;
        role.OrganisationRoleRights = roleRequest.OrganisationRoleRights;
        role.CompanyRoleRights = roleRequest.CompanyRoleRights;
        role.DepartmentRoleRights = roleRequest.DepartmentRoleRights;
        role.TeamRoleRights = roleRequest.TeamRoleRights;
        role.UpdatedBy = roleRequest.UpdatedBy;

        await _roleRepository.UpdateAsync(request.RoleId, role, cancellationToken);
        _logger.LogInformation("Updated role with id: [{roleId}] by user with id: [{userId}]", request.RoleId, request.UserId);
    }
    
    private void HandleRoleRightSourceLevel(RoleRightRequest roleRightRequest, Role role)
    {
        switch (roleRightRequest.SourceLevel)
        {
            case SourceLevel.Organisation:
                var organisationRoleRight = _mapper.Map<OrganisationRoleRight>(roleRightRequest);
                role.OrganisationRoleRights.Add(organisationRoleRight);
                break;
            case SourceLevel.Company:
                var companyRoleRight = _mapper.Map<CompanyRoleRight>(roleRightRequest);
                role.CompanyRoleRights.Add(companyRoleRight);
                break;
            case SourceLevel.Department:
                var departmentRoleRight = _mapper.Map<DepartmentRoleRight>(roleRightRequest);
                role.DepartmentRoleRights.Add(departmentRoleRight);
                break;
            case SourceLevel.Team:
                var teamRoleRight = _mapper.Map<TeamRoleRight>(roleRightRequest);
                role.TeamRoleRights.Add(teamRoleRight);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}