namespace PlanSphere.Core.Features.Rights.DTOs;

public class SourceLevelRightDTO
{
    public bool HasAdministratorRights { get; set; }
    public bool HasEditRights { get; set; }
    public bool HasPureViewRights { get; set; }
    public bool HasViewRights { get; set; }
    public bool HasSetOwnWorkScheduleRights { get; set; }
    public bool HasSetOwnJobTitleRights { get; set; }
    public bool HasSetAutomaticCheckInOutRights { get; set; }
    public bool HasManuallySetOwnWorkTimeRights { get; set; }
    public bool HasManageUsersRights { get; set; }
    public bool HasManageTimesRights { get; set; }
}