using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Abstract;

namespace PlanSphere.Core.Features.WorkSchedules.DTOs;

public class WorkScheduleLookUpDTO : BaseLookUpDTO<ulong>
{
    public SourceLevel SourceLevel { get; set; }
}