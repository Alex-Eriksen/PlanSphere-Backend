using PlanSphere.Core.Interfaces.ActionFilters.LateFilters;
using PlanSphere.Core.Interfaces.Repositories;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Filters.LateFilters;

public class WorkScheduleFilter(IUserRepository userRepository) : IWorkScheduleFilter
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    
    public async Task<bool> IsAllowedToChangeOwnWorkScheduleAsync(ulong userId)
    {
        var user = await _userRepository.GetByIdAsync(userId, CancellationToken.None);

        var rights = user.Roles.Select(x => x.Role).SelectMany(x => x.OrganisationRoleRights.Select(x => x.Right.AsEnum)).ToList();
        rights.AddRange(user.Roles.Select(x => x.Role).SelectMany(x => x.CompanyRoleRights.Select(x => x.Right.AsEnum)).ToList());
        rights.AddRange(user.Roles.Select(x => x.Role).SelectMany(x => x.DepartmentRoleRights.Select(x => x.Right.AsEnum)).ToList());
        rights.AddRange(user.Roles.Select(x => x.Role).SelectMany(x => x.TeamRoleRights.Select(x => x.Right.AsEnum)).ToList());
        
        return rights.Contains(Right.SetOwnWorkSchedule);
    }
}