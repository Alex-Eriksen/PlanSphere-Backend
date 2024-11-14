using MediatR;

namespace PlanSphere.Core.Features.WorkTimes.Commands.CheckOutWorkTime;

public record CheckOutWorkTimeCommand(ulong UserId) : IRequest;