using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.JobTitles.Commands.UpdateJobTitle;

[HandlerType(HandlerType.SystemApi)]
public class UpdateJobTitleCommandHandler(
    IJobTitleRepository jobTitleRepository,
    ILogger<UpdateJobTitleCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<UpdateJobTitleCommand>
{
    private readonly IJobTitleRepository _jobTitleRepository = jobTitleRepository ?? throw new ArgumentNullException(nameof(jobTitleRepository));
    private readonly ILogger<UpdateJobTitleCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(UpdateJobTitleCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Updating job title");
        
        _logger.LogInformation("Fetching job title with id: [{jobTitleId}] on {sourceLevel} with id: [{sourceLevelId}].", command.Id, command.SourceLevel, command.SourceLevelId);
        var jobTitle = await _jobTitleRepository.GetByIdAsync(command.Id, cancellationToken);
        _logger.LogInformation("Fetched job title with id: [{jobTitleId}] on {sourceLevel} with id: [{sourceLevelId}].", command.Id, command.SourceLevel, command.SourceLevelId);
        jobTitle = HandleJobTitleSourceLevel(command, jobTitle);
        var mappedJobTitle = _mapper.Map(command.Request, jobTitle);
        _logger.LogInformation("Updating job title with id: [{jobTitleId}] on {sourceLevel} with id: [{sourceLevelId}].", command.Id, command.SourceLevel, command.SourceLevelId);
        await _jobTitleRepository.UpdateAsync(command.Id, mappedJobTitle, cancellationToken);
        _logger.LogInformation("Updated job title with id: [{jobTitleId}] on {sourceLevel} with id: [{sourceLevelId}].", command.Id, command.SourceLevel, command.SourceLevelId);

    }
    
    private static JobTitle HandleJobTitleSourceLevel(UpdateJobTitleCommand command, JobTitle jobTitle)
    {
        switch (command.SourceLevel)
        {
            case SourceLevel.Organisation:
                jobTitle.OrganisationJobTitle.IsInheritanceActive = command.Request.IsInheritanceActive;
                break;
            case SourceLevel.Company:
                jobTitle.CompanyJobTitle.IsInheritanceActive = command.Request.IsInheritanceActive;
                break;
            case SourceLevel.Department:
                jobTitle.DepartmentJobTitle.IsInheritanceActive = command.Request.IsInheritanceActive;
                break;
            case SourceLevel.Team:
                jobTitle.TeamJobTitle.IsInheritanceActive = command.Request.IsInheritanceActive;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return jobTitle;
    }
}