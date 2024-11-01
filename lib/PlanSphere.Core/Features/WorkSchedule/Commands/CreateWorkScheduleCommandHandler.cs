using AutoMapper;

using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.WorkSchedule.Commands.CreateWorkSchedule;

[HandlerType(HandlerType.SystemApi)]
public class CreateWorkScheduleCommandHandler(
    IWorkScheduleRepository workScheduleRepository,
    ILogger<CreateWorkScheduleCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<CreateWorkScheduleCommand>
{
    private readonly IWorkScheduleRepository _workScheduleRepository = workScheduleRepository ?? throw new ArgumentNullException(nameof(workScheduleRepository));
    private readonly ILogger<CreateWorkScheduleCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task Handle(CreateWorkScheduleCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Creating workschedule on {sourcelevel} with id: [{sourcelevelId}]", command.Request.SourceLevel, command.Request.SourceLevelId);
        var workSchedule = mapper.Map<Domain.Entities.WorkSchedule>(command.Request);
        
        var createdWorkSchedule = await _workScheduleRepository.CreateAsync(workSchedule, cancellationToken);
        _logger.LogInformation("Created workschedule on {sourcelevel} with id: [{sourcelevelId}]", command.Request.SourceLevel, command.Request.SourceLevelId);
    }
}