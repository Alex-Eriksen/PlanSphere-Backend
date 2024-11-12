using PlanSphere.Core.Abstract;
using PlanSphere.Core.Features.Roles.DTOs;
using PlanSphere.Core.Features.WorkSchedules.DTOs;

namespace PlanSphere.Core.Features.Organisations.DTOs;

public class OrganisationSettingsDTO
{
    public RoleDTO DefaultRole { get; set; }
    public WorkScheduleDTO DefaultWorkSchedule { get; set; }
}