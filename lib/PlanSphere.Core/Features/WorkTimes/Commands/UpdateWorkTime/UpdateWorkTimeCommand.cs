using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Features.WorkTimes.Requests;

namespace PlanSphere.Core.Features.WorkTimes.Commands.UpdateWorkTime;

public record UpdateWorkTimeCommand(ulong WorkTimeId, ulong UserId, WorkTimeRequest Request) : IRequest;