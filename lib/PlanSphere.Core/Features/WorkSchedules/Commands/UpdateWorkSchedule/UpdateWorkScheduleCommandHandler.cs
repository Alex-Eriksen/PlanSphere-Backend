using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.WorkSchedules.Commands.UpdateWorkSchedule;

[HandlerType(HandlerType.SystemApi)]
public class UpdateWorkScheduleCommandHandler(
    ILogger<UpdateWorkScheduleCommandHandler> logger,
    IWorkScheduleRepository workScheduleRepository,
    IUserRepository userRepository,
    IMapper mapper
) : IRequestHandler<UpdateWorkScheduleCommand>
{
    private readonly ILogger<UpdateWorkScheduleCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IWorkScheduleRepository _workScheduleRepository = workScheduleRepository ?? throw new ArgumentNullException(nameof(workScheduleRepository));
    private readonly IUserRepository _userRepository = userRepository  ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(UpdateWorkScheduleCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Update work schedule.");
        var workScheduleId = request.WorkScheduleId;
        
        if (!workScheduleId.HasValue)
        {
            _logger.LogInformation("No work schedule id was provided, retrieving work schedule id from user.");
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            workScheduleId = user.Settings.WorkScheduleId;
        }
        
        _logger.LogInformation("Retrieving work schedule with id: [{workScheduleId}]", workScheduleId);
        var workSchedule = await _workScheduleRepository.GetByIdAsync(workScheduleId.Value, cancellationToken);
        _logger.LogInformation("Retrieved work schedule with id: [{workScheduleId}]", workScheduleId);

        workSchedule = _mapper.Map(request, workSchedule);

        _logger.LogInformation("Updating work schedule with id: [{workScheduleId}]", workScheduleId);
        await _workScheduleRepository.UpdateAsync(workScheduleId.Value, workSchedule, cancellationToken);
        _logger.LogInformation("Updated work schedule with id: [{workScheduleId}]", workScheduleId);
    }
}