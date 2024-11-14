using Domain.Entities.EmbeddedEntities;
using MediatR;

namespace PlanSphere.Core.Features.WorkTimes.Commands.DeleteWorkTime;

public record DeleteWorkTimeCommand(ulong WorkTimeId, ulong UserId) : IRequest;