namespace Domain.Entities;

public class UserSettings
{
    public ulong UserId { get; set; }
    
    public bool IsBirthdayPrivate { get; set; }
    public bool IsEmailPrivate { get; set; }
    public bool IsPhoneNumberPrivate { get; set; }
    public bool IsAddressPrivate { get; set; }
    
    public ulong WorkScheduleId { get; set; }
    public bool InheritWorkSchedule { get; set; }
    
    public bool AutoCheckInOut { get; set; }
    public bool AutoCheckOutDisabled { get; set; }
}