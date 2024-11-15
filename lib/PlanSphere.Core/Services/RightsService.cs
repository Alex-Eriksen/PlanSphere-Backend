using Domain.Entities;
using PlanSphere.Core.Interfaces.Services;
using RightEnum = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Services;

public class RightsService : IRightsService
{
    public List<RightEnum> GetOrganisationRights(ulong organisationId, IEnumerable<Role> roles)
    {
        var rights = roles
            .SelectMany(x => x.OrganisationRoleRights)
            .Where(x => x.OrganisationId == organisationId)
            .Select(x => x.Right.AsEnum)
            .ToList();

        rights = rights.Distinct().ToList();
        return rights;
    }

    public List<RightEnum> GetCompanyRights(ulong companyId, IEnumerable<Role> roles)
    {
        var rights = roles
            .SelectMany(x => x.CompanyRoleRights)
            .Where(x => x.CompanyId == companyId)
            .Select(x => x.Right.AsEnum)
            .ToList();

        rights = rights.Distinct().ToList();
        return rights;
    }

    public List<RightEnum> GetDepartmentRights(ulong departmentId, IEnumerable<Role> roles)
    {
        var rights = roles
            .SelectMany(x => x.DepartmentRoleRights)
            .Where(x => x.DepartmentId == departmentId)
            .Select(x => x.Right.AsEnum)
            .ToList();

        rights = rights.Distinct().ToList();
        return rights;
    }

    public List<RightEnum> GetTeamRights(ulong teamId, IEnumerable<Role> roles)
    {
        var rights = roles
            .SelectMany(x => x.TeamRoleRights)
            .Where(x => x.TeamId == teamId)
            .Select(x => x.Right.AsEnum)
            .ToList();

        rights = rights.Distinct().ToList();
        return rights;
    }

    public List<RightEnum> GetUserRights(IEnumerable<Role> roles)
    {
        var enumerable = roles as Role[] ?? roles.ToArray();
        
        var rights = enumerable
            .SelectMany(x => x.OrganisationRoleRights)
            .Select(x => x.Right.AsEnum)
            .ToList();
        
        rights.AddRange(enumerable
            .SelectMany(x => x.CompanyRoleRights)
            .Select(x => x.Right.AsEnum)
            .ToList());
        
        rights.AddRange(enumerable
            .SelectMany(x => x.DepartmentRoleRights)
            .Select(x => x.Right.AsEnum)
            .ToList());
        
        rights.AddRange(enumerable
            .SelectMany(x => x.TeamRoleRights)
            .Select(x => x.Right.AsEnum)
            .ToList());

        rights = rights.Distinct().ToList();
        return rights;
    }
}