using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.JobTitles.Commands.DeleteJobTitle;

[HandlerType(HandlerType.SystemApi)]
public class DeleteJobTitleCommandHandler(
    IJobTitleRepository jobTitleRepository,
    ILogger<DeleteJobTitleCommandHandler> logger
) : IRequestHandler<DeleteJobTitleCommand>
{
    private readonly IJobTitleRepository _jobTitleRepository = jobTitleRepository ?? throw new ArgumentNullException(nameof(jobTitleRepository));
    private readonly ILogger<DeleteJobTitleCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task Handle(DeleteJobTitleCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching job title with id: [{jobTitleId}] on {sourceLevel} with id: [{sourceLevelId}].", request.Id, request.SourceLevel, request.SourceLevelId);
        await _jobTitleRepository.DeleteAsync(request.Id, cancellationToken);
        _logger.LogInformation("Fetched job title with id: [{jobTitleId}] on {sourceLevel} with id: [{sourceLevelId}].", request.Id, request.SourceLevel, request.SourceLevelId);

    }
}