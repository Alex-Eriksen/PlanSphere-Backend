namespace PlanSphere.Core.Features.Users.Requests;

public class UserSettingsPatchRequest
{
    public bool IsBirthdayPrivate { get; set; }
    public bool IsEmailPrivate { get; set; }
    public bool IsPhoneNumberPrivate { get; set; }
    public bool IsAddressPrivate { get; set; }
    
    public bool InheritWorkSchedule { get; set; }
    public bool InheritedWorkScheduleId { get; set; }
    
    public bool AutoCheckInOut { get; set; }
    public bool AutoCheckOutDisabled { get; set; }
}