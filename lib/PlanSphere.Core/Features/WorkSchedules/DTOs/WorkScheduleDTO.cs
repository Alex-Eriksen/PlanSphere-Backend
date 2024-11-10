using PlanSphere.Core.Abstract;

namespace PlanSphere.Core.Features.WorkSchedules.DTOs;

public class WorkScheduleDTO : BaseDTO
{
    public WorkScheduleDTO? Parent { get; set; }
    
    public bool IsDefaultWorkSchedule { get; set; }

    public List<WorkScheduleShiftDTO> WorkScheduleShifts { get; set; } = new List<WorkScheduleShiftDTO>();
}