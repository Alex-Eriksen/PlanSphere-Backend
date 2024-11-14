using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.WorkTimes.Commands.UpdateWorkTime;

[HandlerType(HandlerType.SystemApi)]
public class UpdateWorkTimeCommandHandler(
    IWorkTimeRepository workTimeRepository,
    ILogger<UpdateWorkTimeCommandHandler> logger,
    IWorkTimeLogRepository workTimeLogRepository,
    IMapper mapper
) : IRequestHandler<UpdateWorkTimeCommand>
{
    private readonly IWorkTimeRepository _workTimeRepository = workTimeRepository ?? throw new ArgumentNullException(nameof(workTimeRepository));
    private readonly IWorkTimeLogRepository _workTimeLogRepository = workTimeLogRepository ?? throw new ArgumentNullException(nameof(workTimeLogRepository));
    private readonly ILogger<UpdateWorkTimeCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task Handle(UpdateWorkTimeCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Creating a work time for user");
        
        _logger.LogInformation("Fetching work time with id: [{workTimeId}] from user with id: [{userId}]", command.WorkTimeId, command.UserId);
        var workTime = await _workTimeRepository.GetByIdAsync(command.WorkTimeId, cancellationToken);
        _logger.LogInformation("Fetched work time with id: [{workTimeId}] from user with id: [{userId}]", command.WorkTimeId, command.UserId);

        var workTimeLog = _mapper.Map<WorkTimeLog>(command.Request, opt =>
        {
            opt.Items[MappingKeys.UserId] = command.UserId;
            opt.Items[MappingKeys.ActionType] = ActionType.Update;
            opt.Items[MappingKeys.WorkTime] = workTime;
        });

        _logger.LogInformation("Updating work time with id: [{workTimeId}] from user with id: [{userId}]", command.WorkTimeId, command.UserId);
        var updatedWorkTime = _mapper.Map(command.Request, workTime, opt => opt.Items[MappingKeys.UserId] = command.UserId);
        _logger.LogInformation("Updated work time with id: [{workTimeId}] from user with id: [{userId}]", command.WorkTimeId, command.UserId);

        await _workTimeRepository.UpdateAsync(command.WorkTimeId, updatedWorkTime, cancellationToken);

        _logger.LogInformation("Creating a work time log for user with id: [{userId}].", command.UserId);
        await _workTimeLogRepository.CreateAsync(workTimeLog, cancellationToken);
        _logger.LogInformation("Created a work time log for user with id: [{userId}].", command.UserId);
    }
}