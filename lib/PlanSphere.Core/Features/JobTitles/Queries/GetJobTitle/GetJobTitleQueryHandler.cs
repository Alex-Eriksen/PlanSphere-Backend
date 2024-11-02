using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.JobTitles.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.JobTitles.Queries.GetJobTitle;

[HandlerType(HandlerType.SystemApi)]
public class GetJobTitleQueryHandler(
    IJobTitleRepository jobTitleRepository,
    ILogger<GetJobTitleQueryHandler> logger,
    IMapper mapper
) : IRequestHandler<GetJobTitleQuery, JobTitleDTO>
{
    private readonly IJobTitleRepository _jobTitleRepository = jobTitleRepository ?? throw new ArgumentNullException(nameof(jobTitleRepository));
    private readonly ILogger<GetJobTitleQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<JobTitleDTO> Handle(GetJobTitleQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching a job title");
        
        _logger.LogInformation("Fetching job title with id: [{jobTitleId}].", request.Id);
        var jobTitle = await _jobTitleRepository.GetByIdAsync(request.Id, cancellationToken);
        _logger.LogInformation("Fetched job title with id: [{jobTitleId}].", request.Id);

        var mappedJobTitle = _mapper.Map<JobTitleDTO>(jobTitle);

        return mappedJobTitle;
    }
}