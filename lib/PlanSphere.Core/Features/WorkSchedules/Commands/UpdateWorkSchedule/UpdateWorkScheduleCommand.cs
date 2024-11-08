using MediatR;
using PlanSphere.Core.Features.WorkSchedules.Request;

namespace PlanSphere.Core.Features.WorkSchedules.Commands.UpdateWorkSchedule;

public record UpdateWorkScheduleCommand(ulong? WorkScheduleId, ulong UserId, WorkScheduleRequest Request) : IRequest;
