using PlanSphere.Core.Features.Roles.DTOs;
using PlanSphere.Core.Features.WorkSchedules.DTOs;

namespace PlanSphere.Core.Features.Departments.DTOs;

public class DepartmentSettingsDTO
{
    public WorkScheduleDTO DefaultWorkSchedule { get; set; }
    public RoleDTO DefaultRole { get; set; }
    public bool InheritDefaultWorkSchedule { get; set; }
}