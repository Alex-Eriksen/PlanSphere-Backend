using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.WorkSchedules.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.WorkSchedules.Queries.GetWorkScheduleById;

[HandlerType(HandlerType.SystemApi)]
public class GetWorkScheduleByIdQueryHandler(
    ILogger<GetWorkScheduleByIdQueryHandler> logger,
    IWorkScheduleRepository workScheduleRepository,
    IMapper mapper
) : IRequestHandler<GetWorkScheduleByIdQuery, WorkScheduleDTO>
{
    private readonly ILogger<GetWorkScheduleByIdQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IWorkScheduleRepository _workScheduleRepository = workScheduleRepository ?? throw new ArgumentNullException(nameof(workScheduleRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<WorkScheduleDTO> Handle(GetWorkScheduleByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Get work schedule by id");
        _logger.LogInformation("Retrieving work schedule with id: [{workScheduleId}]", request.WorkScheduleId);
        var workSchedule = await _workScheduleRepository.GetByIdAsync(request.WorkScheduleId, cancellationToken);
        _logger.LogInformation("Retrieved work schedule with id: [{workScheduleId}]", request.WorkScheduleId);

        var workScheduleDto = _mapper.Map<WorkScheduleDTO>(workSchedule);

        return workScheduleDto;
    }
}