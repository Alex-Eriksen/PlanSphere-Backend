using MediatR;

namespace PlanSphere.Core.Features.WorkTimes.Commands.CheckInWorkTime;

public record CheckInWorkTimeCommand(ulong UserId) : IRequest;