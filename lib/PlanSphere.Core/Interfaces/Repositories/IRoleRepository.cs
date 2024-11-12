using Domain.Entities;

namespace PlanSphere.Core.Interfaces.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    Task<List<Right>> GetRightsAsync(CancellationToken cancellationToken);
    Task<Role> ToggleRoleInheritanceAsync(Role role, CancellationToken cancellationToken);
    IQueryable<Role> GetCompanyRoles(ulong companyId, ulong organisationId, IQueryable<Role> query);
    IQueryable<Role> GetDepartmentRoles(ulong departmentId, IQueryable<Role> query);
    IQueryable<Role> GetTeamRoles(ulong teamId, IQueryable<Role> query);
}