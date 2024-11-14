using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.WorkTimes.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.WorkTimes.Queries.GetWorkTimes;

[HandlerType(HandlerType.SystemApi)]
public class GetWorkTimesQueryHandler(
    IWorkTimeRepository workTimeRepository,
    ILogger<GetWorkTimesQueryHandler> logger,
    IMapper mapper
) : IRequestHandler<GetWorkTimesQuery, List<WorkTimeDTO>>
{
    private readonly IWorkTimeRepository _workTimeRepository = workTimeRepository ?? throw new ArgumentNullException(nameof(workTimeRepository));
    private readonly ILogger<GetWorkTimesQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<WorkTimeDTO>> Handle(GetWorkTimesQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching work times from user");
        _logger.LogInformation("Fetching work times from user with id: [{userId}]", request.UserId);
        var workTimes = await _workTimeRepository.GetWorkTimesWithinMonthByUserIdAsync(request.UserId, request.Year, request.Month, cancellationToken);
        _logger.LogInformation("Fetched work times from user with id: [{userId}]", request.UserId);

        var mappedWorkTimes = _mapper.Map<List<WorkTimeDTO>>(workTimes);

        return mappedWorkTimes;
    }
}