using Domain.Entities;
using RightEnum = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Interfaces.Services;

public interface IRightsService
{
    List<RightEnum> GetOrganisationRights(ulong organisationId, IEnumerable<Role> roles);
    List<RightEnum> GetCompanyRights(ulong companyId, IEnumerable<Role> roles);
    List<RightEnum> GetDepartmentRights(ulong departmentId, IEnumerable<Role> roles);
    List<RightEnum> GetTeamRights(ulong teamId, IEnumerable<Role> roles);
    List<RightEnum> GetUserRights(IEnumerable<Role> roles);
}