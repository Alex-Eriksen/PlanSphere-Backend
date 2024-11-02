using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.JobTitles.Commands.CreateJobTitle;

[HandlerType(HandlerType.SystemApi)]
public class CreateJobTitleCommandHandler(
    IJobTitleRepository jobTitleRepository,
    ILogger<CreateJobTitleCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<CreateJobTitleCommand>
{
    private readonly IJobTitleRepository _jobTitleRepository = jobTitleRepository ?? throw new ArgumentNullException(nameof(jobTitleRepository));
    private readonly ILogger<CreateJobTitleCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task Handle(CreateJobTitleCommand command, CancellationToken cancellationToken)
    {
        var jobTitle = _mapper.Map<JobTitle>(command);
        HandleJobTitleSourceLevel(command, jobTitle);

        _logger.BeginScope("Creating job title on {sourceLevel} with id: [{sourceLevelId}].", command.SourceLevel, command.SourceLevelId);
        await _jobTitleRepository.CreateAsync(jobTitle, cancellationToken);
        _logger.LogInformation("Created job title on {sourceLevel} with id: [{sourceLevelId}].", command.SourceLevel, command.SourceLevelId);
    }
    
    private static void HandleJobTitleSourceLevel(CreateJobTitleCommand request, JobTitle jobTitle)
    {
        switch (request.SourceLevel)
        {
            case SourceLevel.Organisation:
                jobTitle.OrganisationJobTitle = new OrganisationJobTitle()
                {
                    OrganisationId = request.SourceLevelId,
                    IsInheritanceActive = request.Request.IsInheritanceActive
                };
                break;
            case SourceLevel.Company:
                jobTitle.CompanyJobTitle = new CompanyJobTitle()
                {
                    CompanyId = request.SourceLevelId,
                    IsInheritanceActive = request.Request.IsInheritanceActive
                };
                break;
            case SourceLevel.Department:
                jobTitle.DepartmentJobTitle = new DepartmentJobTitle()
                {
                    DepartmentId = request.SourceLevelId,
                    IsInheritanceActive = request.Request.IsInheritanceActive
                };
                break;
            case SourceLevel.Team:
                jobTitle.TeamJobTitle = new TeamJobTitle()
                {
                    TeamId = request.SourceLevelId,
                    IsInheritanceActive = request.Request.IsInheritanceActive
                };
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}