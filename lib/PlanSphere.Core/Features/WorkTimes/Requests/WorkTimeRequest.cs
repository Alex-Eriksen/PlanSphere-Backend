using Domain.Entities.EmbeddedEntities;

namespace PlanSphere.Core.Features.WorkTimes.Requests;

public class WorkTimeRequest
{
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public WorkTimeType WorkTimeType { get; set; }
    public ShiftLocation Location { get; set; }
    public ulong? LoggedBy { get; set; }
}