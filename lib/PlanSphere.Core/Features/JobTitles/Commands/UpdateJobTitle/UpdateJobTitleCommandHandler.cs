using AutoMapper;
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
        _logger.BeginScope("Fetching job title with id: [{jobTitleId}] on {sourceLevel} with id: [{sourceLevelId}].", command.Id, command.SourceLevel, command.SourceLevelId);
        var jobTitle = await _jobTitleRepository.GetByIdAsync(command.Id, cancellationToken);
        _logger.LogInformation("Fetched job title with id: [{jobTitleId}] on {sourceLevel} with id: [{sourceLevelId}].", command.Id, command.SourceLevel, command.SourceLevelId);
            
        var mappedJobTitle = _mapper.Map(command, jobTitle);

        _logger.LogInformation("Updating job title with id: [{jobTitleId}] on {sourceLevel} with id: [{sourceLevelId}].", command.Id, command.SourceLevel, command.SourceLevelId);
        await _jobTitleRepository.UpdateAsync(command.Id, mappedJobTitle, cancellationToken);
        _logger.LogInformation("Updated job title with id: [{jobTitleId}] on {sourceLevel} with id: [{sourceLevelId}].", command.Id, command.SourceLevel, command.SourceLevelId);

    }
}