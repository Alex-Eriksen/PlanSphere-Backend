using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Features.WorkTimes.Queries.GetTotalWorkTime;

public record GetTotalWorkTimeQuery(WorkTimeType WorkTimeType, Periods Period) : IRequest<string>
{
    public ulong UserId { get; set; }
}