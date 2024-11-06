using PlanSphere.Core.Features.WorkSchedules.DTOs;

namespace PlanSphere.Core.Features.Users.DTOs;

public class UserSettingsDTO
{
    public bool IsBirthdayPrivate { get; set; }
    public bool IsEmailPrivate { get; set; }
    public bool IsPhoneNumberPrivate { get; set; }
    public bool IsAddressPrivate { get; set; }
    
    public WorkScheduleDTO WorkSchedule { get; set; }
    public bool InheritWorkSchedule { get; set; }
    
    public bool AutoCheckInOut { get; set; }
    public bool AutoCheckOutDisabled { get; set; }
}