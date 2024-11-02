using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
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
        
        _logger.LogInformation("Toggling inheritance on job title with id: [{jobTitleId}]", request.JobTitleId);
        var isInheritance = await _jobTitleRepository.ToggleInheritanceAsync(request.JobTitleId, cancellationToken);
        _logger.LogInformation("Toggled inheritance on job title with id: [{jobTitleId}]", request.JobTitleId);

        return isInheritance;
    }
}