using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.WorkTimes.Commands.DeleteWorkTime;

[HandlerType(HandlerType.SystemApi)]
public class DeleteWorkTimeCommandHandler(
    IWorkTimeRepository workTimeRepository,
    ILogger<DeleteWorkTimeCommandHandler> logger,
    IWorkTimeLogRepository workTimeLogRepository,
    IMapper mapper
) : IRequestHandler<DeleteWorkTimeCommand>
{
    private readonly IWorkTimeRepository _workTimeRepository = workTimeRepository ?? throw new ArgumentNullException(nameof(workTimeRepository));
    private readonly IWorkTimeLogRepository _workTimeLogRepository = workTimeLogRepository ?? throw new ArgumentNullException(nameof(workTimeLogRepository));
    private readonly ILogger<DeleteWorkTimeCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task Handle(DeleteWorkTimeCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting work time from user with id: [{userId}]", command.UserId);
        var workItem = await _workTimeRepository.DeleteAsync(command.WorkTimeId, cancellationToken);
        _logger.LogInformation("Deleted work time from user with id: [{userId}]", command.UserId);

        var workTimeLog = _mapper.Map<WorkTimeLog>(workItem, opt =>
        {
            opt.Items[MappingKeys.ActionType] = ActionType.Delete;
        });
        
        _logger.LogInformation("Creating a work time log for user with id: [{userId}].", workItem.UserId);
        await _workTimeLogRepository.CreateAsync(workTimeLog, cancellationToken);
        _logger.LogInformation("Created a work time log for user with id: [{userId}].", workItem.UserId);
    }
}