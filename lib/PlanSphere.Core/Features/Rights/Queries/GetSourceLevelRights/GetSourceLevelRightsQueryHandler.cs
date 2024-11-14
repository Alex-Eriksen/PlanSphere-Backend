using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Rights.DTOs;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Features.Rights.Queries.GetSourceLevelRights;

[HandlerType(HandlerType.SystemApi)]
public class GetSourceLevelRightsQueryHandler(
    ILogger<GetSourceLevelRightsQueryHandler> logger,
    IUserRepository userRepository,
    IMapper mapper,
    ICompanyRepository companyRepository,
    IDepartmentRepository departmentRepository,
    ITeamRepository teamRepository,
    IRightsService rightsService
) : IRequestHandler<GetSourceLevelRightsQuery, SourceLevelRightDTO>
{
    private readonly ILogger<GetSourceLevelRightsQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IDepartmentRepository _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    private readonly ITeamRepository _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
    private readonly IRightsService _rightsService = rightsService ?? throw new ArgumentNullException(nameof(rightsService));

    public async Task<SourceLevelRightDTO> Handle(GetSourceLevelRightsQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Getting source level rights.");
        _logger.LogInformation("Retrieving source level right for source level of type [{sourceLevel}] with id: [{sourceLevelId}] for user with id: [{userId}]", request.SourceLevel, request.SourceLevelId, request.UserId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

        var sourceLevelRightDto = request.SourceLevel switch
        {
            SourceLevel.Organisation => GetOrganisationRights(request.SourceLevelId!.Value, user),
            SourceLevel.Company => await GetCompanyRightsAsync(request.SourceLevelId!.Value, user, cancellationToken),
            SourceLevel.Department => await GetDepartmentRightsAsync(request.SourceLevelId!.Value, user, cancellationToken),
            SourceLevel.Team => await GetTeamRightsAsync(request.SourceLevelId!.Value, user, cancellationToken),
            _ => GetUserRightsAsync(user)
        };

        _logger.LogInformation("Retrieved source level right for source level of type [{sourceLevel}] with id: [{sourceLevelId}] for user with id: [{userId}]", request.SourceLevel, request.SourceLevelId, request.UserId);

        return sourceLevelRightDto;
    }

    private SourceLevelRightDTO GetOrganisationRights(ulong organisationId, User user)
    {
        var roles = user.Roles.Select(x => x.Role);
        var rights = _rightsService.GetOrganisationRights(organisationId, roles);

        return _mapper.Map<SourceLevelRightDTO>(rights);
    }

    private async Task<SourceLevelRightDTO> GetCompanyRightsAsync(ulong companyId, User user, CancellationToken cancellationToken)
    {
        var roles = user.Roles.Select(x => x.Role).ToList();
        var company = await _companyRepository.GetByIdAsync(companyId, cancellationToken);
        
        var rights = _rightsService.GetOrganisationRights(company.OrganisationId, roles);
        rights.AddRange(_rightsService.GetCompanyRights(companyId, roles));

        rights = rights.Distinct().ToList();
        
        return _mapper.Map<SourceLevelRightDTO>(rights);
    }
    
    private async Task<SourceLevelRightDTO> GetDepartmentRightsAsync(ulong departmentId, User user, CancellationToken cancellationToken)
    {
        var roles = user.Roles.Select(x => x.Role).ToList();
        var department = await _departmentRepository.GetByIdAsync(departmentId, cancellationToken);

        var rights = _rightsService.GetOrganisationRights(department.Company.OrganisationId, roles);
        rights.AddRange(_rightsService.GetCompanyRights(department.CompanyId, roles));
        rights.AddRange(_rightsService.GetDepartmentRights(departmentId, roles));

        rights = rights.Distinct().ToList();

        return _mapper.Map<SourceLevelRightDTO>(rights);
    }
    
    private async Task<SourceLevelRightDTO> GetTeamRightsAsync(ulong teamId, User user, CancellationToken cancellationToken)
    {
        var roles = user.Roles.Select(x => x.Role).ToList();
        var team = await _teamRepository.GetByIdAsync(teamId, cancellationToken);

        var rights = _rightsService.GetOrganisationRights(team.Department.Company.OrganisationId, roles);
        rights.AddRange(_rightsService.GetCompanyRights(team.Department.CompanyId, roles));
        rights.AddRange(_rightsService.GetDepartmentRights(team.DepartmentId, roles));
        rights.AddRange(_rightsService.GetTeamRights(teamId, roles));

        rights = rights.Distinct().ToList();

        return _mapper.Map<SourceLevelRightDTO>(rights);
    }
    
    private SourceLevelRightDTO GetUserRightsAsync(User user)
    {
        var roles = user.Roles.Select(x => x.Role);
        var rights = _rightsService.GetUserRights(roles);

        return _mapper.Map<SourceLevelRightDTO>(rights);
    }
}