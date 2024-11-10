using Domain.Entities;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Features.Roles.DTOs;
using PlanSphere.Core.Features.WorkSchedules.DTOs;

namespace PlanSphere.Core.Features.Teams.DTOs;

public class TeamSettingsDTO : BaseDTO
{
    public RoleDTO DefaultRole { get; set; }
    public WorkScheduleDTO DefaultWorkSchedule { get; set; }
}