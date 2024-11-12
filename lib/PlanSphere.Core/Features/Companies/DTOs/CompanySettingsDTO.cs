using PlanSphere.Core.Features.Roles.DTOs;
using PlanSphere.Core.Features.WorkSchedules.DTOs;

namespace PlanSphere.Core.Features.Companies.DTOs;

public class CompanySettingsDTO
{
    public RoleDTO DefaultRole { get; set; }
    public WorkScheduleDTO DefaultWorkSchedule { get; set; }
    public bool InheritDefaultWorkSchedule { get; set; }
}