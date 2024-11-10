using Domain.Entities;
using PlanSphere.Core.Abstract;

namespace PlanSphere.Core.Features.Teams.DTOs;

public class TeamSettingsDTO : BaseDTO
{
    public virtual Team Team { get; set; }
    public ulong DefaultRoleId { get; set; }
    public virtual Role DefaultRole { get; set; }
    public ulong DefaultWorkScheduleId { get; set; }
    public virtual WorkSchedule DefaultWorkSchedule { get; set; }
    
    public bool InheritDefaultWorkSchedule { get; set; }
}