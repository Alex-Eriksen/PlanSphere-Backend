using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using Microsoft.AspNetCore.Http;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Exceptions;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Interfaces.ActionFilters.LateFilters;
using PlanSphere.Core.Interfaces.Repositories;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Filters.LateFilters;

public class RoleFilter(IUserRepository userRepository) : IRoleFilter
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    
    public async Task CheckIsAuthorizedSourceLevelAsync(HttpContext context, Right requiredRight, SourceLevel? targetedLayer = null, bool isSpecific = false)
    {
        var user = await _userRepository.GetByIdAsync(context.User.GetUserId(), CancellationToken.None);
        var userRoles = user.Roles.Select(x => x.Role).ToList();
        var sourceLevelId = context.GetSourceLevelId();
        var sourceLevel = targetedLayer ?? context.GetSourceLevel();

        var authorized = false;

        if (userRoles.Select(x => x.Name).Contains(PermissionFilterConstants.SystemAdministratorKey))
        {
            return;
        }
        
        authorized = isSpecific ? AuthorizedSpecific(userRoles, sourceLevel, sourceLevelId, authorized, requiredRight) : Authorized(userRoles, sourceLevel, sourceLevelId, authorized, requiredRight);

        if (!authorized)
        {
            throw new ForbiddenAccessException(ErrorMessageConstants.UnauthorizedActionMessage);
        }
    }
    
    private bool AuthorizedSpecific(List<Role> userRoles, SourceLevel sourceLevel, ulong sourceLevelId, bool authorized, Right requiredRight)
    {
        foreach (var role in userRoles)
        {
            switch (sourceLevel)
            {
                case SourceLevel.Organisation:
                    if (role.OrganisationRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.OrganisationId == sourceLevelId) == null)
                    {
                        continue;
                    }

                    authorized = true;
                    break;
                case SourceLevel.Company:
                    if (role.OrganisationRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.Organisation.Companies.Any(y => y.Id == sourceLevelId)) != null)
                    {
                        authorized = true;
                        break;
                    }
                    if (role.CompanyRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.CompanyId == sourceLevelId) == null)
                    {
                        continue;
                    }

                    authorized = true;
                    break;
                case SourceLevel.Department:
                    if (role.OrganisationRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.Organisation.Companies.SelectMany(x => x.Departments).Any(y => y.Id == sourceLevelId)) != null)
                    {
                        authorized = true;
                        break;
                    }
                    if (role.CompanyRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.Company.Departments.Any(y => y.Id == sourceLevelId)) != null)
                    {
                        authorized = true;
                        break;
                    }
                    if (role.DepartmentRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.DepartmentId == sourceLevelId) == null)
                    {
                        continue;
                    }

                    authorized = true;
                    break;
                case SourceLevel.Team:
                    if (role.OrganisationRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.Organisation.Companies.SelectMany(x => x.Departments).SelectMany(z => z.Teams).Any(y => y.Id == sourceLevelId)) != null)
                    {
                        authorized = true;
                        break;
                    }
                    if (role.CompanyRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.Company.Departments.SelectMany(z => z.Teams).Any(y => y.Id == sourceLevelId)) != null)
                    {
                        authorized = true;
                        break;
                    }
                    if (role.DepartmentRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.Department.Teams.Any(y => y.Id == sourceLevelId)) != null)
                    {
                        authorized = true;
                        break;
                    }
                    if (role.TeamRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.TeamId == sourceLevelId) == null)
                    {
                        continue;
                    }

                    authorized = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return authorized;
    }
    
    private bool Authorized(List<Role> userRoles, SourceLevel sourceLevel, ulong sourceLevelId, bool authorized, Right requiredRight)
    {
        foreach (var role in userRoles)
        {
            switch (sourceLevel)
            {
                case SourceLevel.Organisation:
                    if (!role.OrganisationRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.OrganisationId == sourceLevelId))
                    {
                        continue;
                    }

                    authorized = true;
                    break;
                case SourceLevel.Company:
                    if (role.OrganisationRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.Organisation.Companies.Any(y => y.Id == sourceLevelId)))
                    {
                        authorized = true;
                        break;
                    }
                    if (!role.CompanyRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.CompanyId == sourceLevelId))
                    {
                        continue;
                    }

                    authorized = true;
                    break;
                case SourceLevel.Department:
                    if (role.OrganisationRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.Organisation.Companies.SelectMany(x => x.Departments).Any(y => y.Id == sourceLevelId)))
                    {
                        authorized = true;
                        break;
                    }
                    if (role.CompanyRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.Company.Departments.Any(y => y.Id == sourceLevelId)))
                    {
                        authorized = true;
                        break;
                    }
                    if (!role.DepartmentRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.DepartmentId == sourceLevelId))
                    {
                        continue;
                    }

                    authorized = true;
                    break;
                case SourceLevel.Team:
                    if (role.OrganisationRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.Organisation.Companies.SelectMany(x => x.Departments).SelectMany(z => z.Teams).Any(y => y.Id == sourceLevelId)))
                    {
                        authorized = true;
                        break;
                    }
                    if (role.CompanyRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.Company.Departments.SelectMany(z => z.Teams).Any(y => y.Id == sourceLevelId)))
                    {
                        authorized = true;
                        break;
                    }
                    if (role.DepartmentRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.Department.Teams.Any(y => y.Id == sourceLevelId)))
                    {
                        authorized = true;
                        break;
                    }
                    if (!role.TeamRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.TeamId == sourceLevelId))
                    {
                        continue;
                    }

                    authorized = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return authorized;
    }
}