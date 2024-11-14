using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.WorkTimes.Commands.CreateWorkTime;

[HandlerType(HandlerType.SystemApi)]
public class CreateWorkTimeCommandHandler(
    IWorkTimeRepository workTimeRepository,
    ILogger<CreateWorkTimeCommandHandler> logger,
    IWorkTimeLogRepository workTimeLogRepository,
    IMapper mapper
) : IRequestHandler<CreateWorkTimeCommand>
{
    private readonly IWorkTimeRepository _workTimeRepository = workTimeRepository ?? throw new ArgumentNullException(nameof(workTimeRepository));
    private readonly IWorkTimeLogRepository _workTimeLogRepository = workTimeLogRepository ?? throw new ArgumentNullException(nameof(workTimeLogRepository));
    private readonly ILogger<CreateWorkTimeCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(CreateWorkTimeCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Creating a work time for user");
        var workTime = _mapper.Map<WorkTime>(command.Request, opt => opt.Items[MappingKeys.UserId] = command.UserId);
        
        _logger.LogInformation("Creating a work time for user with id: [{userId}].", command.UserId);
        await _workTimeRepository.CreateAsync(workTime, cancellationToken);
        _logger.LogInformation("Created a work time for user with id: [{userId}].", command.UserId);

        var workTimeLog = _mapper.Map<WorkTimeLog>(command.Request, opt =>
        {
            opt.Items[MappingKeys.UserId] = command.UserId;
            opt.Items[MappingKeys.ActionType] = ActionType.Create;
        });
        
        _logger.LogInformation("Creating a work time log for user with id: [{userId}].", command.UserId);
        await _workTimeLogRepository.CreateAsync(workTimeLog, cancellationToken);
        _logger.LogInformation("Created a work time log for user with id: [{userId}].", command.UserId);
    }
}