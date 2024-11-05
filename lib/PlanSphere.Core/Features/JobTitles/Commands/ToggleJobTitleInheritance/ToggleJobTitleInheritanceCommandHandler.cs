using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.JobTitles.Commands.ToggleJobTitleInheritance;

[HandlerType(HandlerType.SystemApi)]
public class ToggleJobTitleInheritanceCommandHandler(
    IJobTitleRepository jobTitleRepository,
    ILogger<ToggleJobTitleInheritanceCommandHandler> logger
) : IRequestHandler<ToggleJobTitleInheritanceCommand, bool>
{
    private readonly IJobTitleRepository _jobTitleRepository = jobTitleRepository ?? throw new ArgumentNullException(nameof(jobTitleRepository));
    private readonly ILogger<ToggleJobTitleInheritanceCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<bool> Handle(ToggleJobTitleInheritanceCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Toggling inheritance on job title");
        var jobTitle = await _jobTitleRepository.GetByIdAsync(request.JobTitleId, cancellationToken);

        if (request.SourceLevel == jobTitle.SourceLevel)
        {
            _logger.LogInformation("Toggling inheritance on job title with id: [{jobTitleId}]", request.JobTitleId);
            var isInheritance = await _jobTitleRepository.ToggleInheritanceAsync(request.JobTitleId, cancellationToken);
            _logger.LogInformation("Toggled inheritance on job title with id: [{jobTitleId}]", request.JobTitleId);
            return isInheritance;
        }

        var isInherited = ToggleBlockedJobTitle(request, jobTitle);
        
        _logger.LogInformation("Toggled blocked on job title with id: [{jobTitleId}]", request.JobTitleId);
        await _jobTitleRepository.UpdateAsync(jobTitle.Id, jobTitle, cancellationToken);
        _logger.LogInformation("Toggled blocked on job title with id: [{jobTitleId}]", request.JobTitleId);
        return isInherited;
    }

    private bool ToggleBlockedJobTitle(ISourceLevelRequest command, JobTitle jobTitle)
    {
        switch (command.SourceLevel)
        {
            case SourceLevel.Company:
                var companyBlockedJobTitle = jobTitle.CompanyBlockedJobTitles.SingleOrDefault(x => x.JobTitleId == jobTitle.Id && x.CompanyId == command.SourceLevelId);
                if (companyBlockedJobTitle != null)
                {
                    jobTitle.CompanyBlockedJobTitles.Remove(companyBlockedJobTitle);
                    return true;
                }
                jobTitle.CompanyBlockedJobTitles.Add(new CompanyBlockedJobTitle() { JobTitleId = jobTitle.Id, CompanyId = command.SourceLevelId});
                break;
            case SourceLevel.Department:
                var departmentBlockedJobTitle = jobTitle.DepartmentBlockedJobTitles.SingleOrDefault(x => x.JobTitleId == jobTitle.Id && x.DepartmentId == command.SourceLevelId);
                if (departmentBlockedJobTitle != null)
                {
                    jobTitle.DepartmentBlockedJobTitles.Remove(departmentBlockedJobTitle);
                    return true;
                }
                jobTitle.DepartmentBlockedJobTitles.Add(new DepartmentBlockedJobTitle() { JobTitleId = jobTitle.Id, DepartmentId = command.SourceLevelId});
                break;
        }
        return false;
    }
}