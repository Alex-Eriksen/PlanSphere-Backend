using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Features.WorkTimes.Requests;

namespace PlanSphere.Core.Features.WorkTimes.Commands.CreateWorkTime;

public record CreateWorkTimeCommand(WorkTimeRequest Request, ulong UserId, ActionType ActionType) : IRequest;