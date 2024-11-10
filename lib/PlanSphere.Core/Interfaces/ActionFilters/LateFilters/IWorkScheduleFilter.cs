namespace PlanSphere.Core.Interfaces.ActionFilters.LateFilters;

public interface IWorkScheduleFilter
{
    Task<bool> IsAllowedToChangeOwnWorkScheduleAsync(ulong userId);
}