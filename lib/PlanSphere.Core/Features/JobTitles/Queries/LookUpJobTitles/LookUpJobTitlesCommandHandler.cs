using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.JobTitles.DTOs;
using PlanSphere.Core.Interfaces.Repositories;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Features.JobTitles.Queries.LookUpJobTitles;

[HandlerType(HandlerType.SystemApi)]
public class LookUpJobTitlesCommandHandler(
    ILogger<LookUpJobTitlesCommandHandler> logger,
    IUserRepository userRepository,
    IMapper mapper
) : IRequestHandler<LookUpJobTitlesCommand, List<JobTitleLookUpDTO>>
{
    private readonly ILogger<LookUpJobTitlesCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<JobTitleLookUpDTO>> Handle(LookUpJobTitlesCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Look up job titles.");
        _logger.LogInformation("Retrieving job titles available for user with id: [{userId}]", request.UserId);

        var user = await _userRepository.GetByIdWithRolesAndJobTitlesAsync(request.UserId, cancellationToken);
        var roles = user.Roles.Select(x => x.Role).ToList();
        
        var jobTitles = new List<JobTitle>();
        jobTitles.AddRange(GetJobTitlesFromRoles(roles, Right.View, x => x.OrganisationRoleRights, x => x.Organisation.JobTitles.Select(x => x.JobTitle)));
        jobTitles.AddRange(GetJobTitlesFromRoles(roles, Right.View, x => x.OrganisationRoleRights, x => x.Organisation.Companies.SelectMany(c => c.JobTitles).Select(x => x.JobTitle)));
        jobTitles.AddRange(GetJobTitlesFromRoles(roles, Right.View, x => x.OrganisationRoleRights, x => x.Organisation.Companies.SelectMany(c => c.Departments.SelectMany(d => d.JobTitles).Select(x => x.JobTitle))));
        jobTitles.AddRange(GetJobTitlesFromRoles(roles, Right.View, x => x.OrganisationRoleRights, x => x.Organisation.Companies.SelectMany(c => c.Departments.SelectMany(d => d.Teams.SelectMany(t => t.JobTitles).Select(x => x.JobTitle)))));
        
        jobTitles.AddRange(GetJobTitlesFromRoles(roles, Right.View, x => x.CompanyRoleRights, x => x.Company.Departments.SelectMany(d => d.Teams.SelectMany(t => t.JobTitles).Select(x => x.JobTitle))));
        jobTitles.AddRange(GetJobTitlesFromRoles(roles, Right.View, x => x.CompanyRoleRights, x => x.Company.Departments.SelectMany(d => d.JobTitles).Select(x => x.JobTitle)));
        jobTitles.AddRange(GetJobTitlesFromRoles(roles, Right.View, x => x.CompanyRoleRights, x => x.Company.JobTitles.Select(x => x.JobTitle)));
        
        jobTitles.AddRange(GetJobTitlesFromRoles(roles, Right.View, x => x.DepartmentRoleRights, x => x.Department.JobTitles.Select(x => x.JobTitle)));
        jobTitles.AddRange(GetJobTitlesFromRoles(roles, Right.View, x => x.DepartmentRoleRights, x => x.Department.Teams.SelectMany(t => t.JobTitles).Select(x => x.JobTitle)));
        
        jobTitles.AddRange(GetJobTitlesFromRoles(roles, Right.View, x => x.TeamRoleRights, x => x.Team.JobTitles.Select(x => x.JobTitle)));

        _logger.LogInformation("Retrieved job titles available for user with id: [{userId}]", request.UserId);

        var distinctJobTitles = jobTitles.Distinct().ToList();
        var jobTitleLookUpDtos = _mapper.Map<List<JobTitleLookUpDTO>>(distinctJobTitles);

        return jobTitleLookUpDtos;
    }

    private IEnumerable<JobTitle> GetJobTitlesFromRoles<T>(
        IEnumerable<Role> roles,
        Right accessRight,
        Func<Role, IEnumerable<T>> rightsSelector,
        Func<T, IEnumerable<JobTitle>> jobTitlesSelector
    ) where T : IRoleRight
    {
        return roles
            .SelectMany(rightsSelector)
            .Where(right => right.Right.AsEnum <= accessRight)
            .SelectMany(jobTitlesSelector)
            .ToList();
    }
}
