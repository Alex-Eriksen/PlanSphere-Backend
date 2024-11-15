using AutoMapper;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.JobTitles.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

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
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        var roles = user.Roles.Select(x => x.Role).ToList();

        var jobTitles = roles
            .SelectMany(x => x.OrganisationRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Organisation)
            .SelectMany(x => x.JobTitles)
            .Select(x => x.JobTitle)
            .ToList();
        
        jobTitles.AddRange(roles
            .SelectMany(x => x.CompanyRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Company)
            .SelectMany(x => x.JobTitles)
            .Select(x => x.JobTitle)
            .ToList()
        );
        
        jobTitles.AddRange(roles
            .SelectMany(x => x.DepartmentRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Department)
            .SelectMany(x => x.JobTitles)
            .Select(x => x.JobTitle)
            .ToList()
        );
        
        jobTitles.AddRange(roles
            .SelectMany(x => x.TeamRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Team)
            .SelectMany(x => x.JobTitles)
            .Select(x => x.JobTitle)
            .ToList()
        );
        
        _logger.LogInformation("Retrieved job titles available for user with id: [{userId}]", request.UserId);
        jobTitles = jobTitles.Distinct().ToList();
        var jobTitleLookUpDtos = _mapper.Map<List<JobTitleLookUpDTO>>(jobTitles);

        return jobTitleLookUpDtos;
    }
}