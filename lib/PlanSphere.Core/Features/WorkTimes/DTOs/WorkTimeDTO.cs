using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Abstract;

namespace PlanSphere.Core.Features.WorkTimes.DTOs;

public class WorkTimeDTO : BaseDTO
{
    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public WorkTimeType WorkTimeType { get; set; }
    public ShiftLocation? Location { get; set; }
}