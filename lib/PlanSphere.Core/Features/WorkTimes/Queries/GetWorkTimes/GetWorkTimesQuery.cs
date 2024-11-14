using MediatR;
using Newtonsoft.Json;
using PlanSphere.Core.Features.WorkTimes.DTOs;

namespace PlanSphere.Core.Features.WorkTimes.Queries.GetWorkTimes;

public record GetWorkTimesQuery(int Year, int Month) : IRequest<List<WorkTimeDTO>>
{
    [JsonIgnore]
    public ulong UserId { get; set; }
}