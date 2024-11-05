using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.Requests;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Roles.Commands.CreateRole;

[HandlerType(HandlerType.SystemApi)]
public class CreateRoleCommandHandler(
    IRoleRepository roleRepository,
    IMapper mapper
) : IRequestHandler<CreateRoleCommand>
{
    private readonly IRoleRepository _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = _mapper.Map<Role>(request);

        HandleRoleSourceLevel(request, role);
        
        foreach (var roleRightRequest in request.Request.Rights)
        {
            HandleRoleRightSourceLevel(roleRightRequest, role);
        }
        
        await _roleRepository.CreateAsync(role, cancellationToken);
    }

    private static void HandleRoleSourceLevel(CreateRoleCommand request, Role role)
    {
        switch (request.SourceLevel)
        {
            case SourceLevel.Organisation:
                role.OrganisationRole = new OrganisationRole()
                {
                    OrganisationId = request.SourceLevelId
                };
                break;
            case SourceLevel.Company:
                role.CompanyRole = new CompanyRole()
                {
                    CompanyId = request.SourceLevelId
                };
                break;
            case SourceLevel.Department:
                role.DepartmentRole = new DepartmentRole()
                {
                    DepartmentId = request.SourceLevelId
                };
                break;
            case SourceLevel.Team:
                role.TeamRole = new TeamRole()
                {
                    TeamId = request.SourceLevelId
                };
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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