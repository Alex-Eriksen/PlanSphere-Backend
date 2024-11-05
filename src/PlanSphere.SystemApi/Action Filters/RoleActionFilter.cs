using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using Microsoft.AspNetCore.Mvc.Filters;
using PlanSphere.Core.Exceptions;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.SystemApi.Extensions;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.SystemApi.Action_Filters;

public class RoleActionFilter(
    IUserRepository userRepository,
    Right requiredRight,
    bool isSpecific = false
) : ActionFilterAttribute
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = await _userRepository.GetByIdAsync(context.HttpContext.User.GetUserId(), CancellationToken.None);
        var userRoles = user.Roles.Select(x => x.Role).ToList();
        var sourceLevelId = context.HttpContext.GetSourceLevelId();
        var sourceLevel = context.HttpContext.GetSourceLevel();

        var authorized = false;

        authorized = isSpecific ? AuthorizedSpecific(userRoles, sourceLevel, sourceLevelId, authorized) : Authorized(userRoles, sourceLevel, sourceLevelId, authorized);

        if (!authorized)
        {
            throw new ForbiddenAccessException("You are not authorized to perform this action!");
        }

        await next();
    }

    private bool AuthorizedSpecific(List<Role> userRoles, SourceLevel sourceLevel, ulong sourceLevelId, bool authorized)
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
                    if (role.OrganisationRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.OrganisationId == sourceLevelId) == null)
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
                    if (role.OrganisationRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.OrganisationId == sourceLevelId) == null)
                    {
                        authorized = true;
                        break;
                    }
                    if (role.CompanyRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.CompanyId == sourceLevelId) == null)
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
                    if (role.OrganisationRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.OrganisationId == sourceLevelId) == null)
                    {
                        authorized = true;
                        break;
                    }
                    if (role.CompanyRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.CompanyId == sourceLevelId) == null)
                    {
                        authorized = true;
                        break;
                    }
                    if (role.DepartmentRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.DepartmentId == sourceLevelId) == null)
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
    
    private bool Authorized(List<Role> userRoles, SourceLevel sourceLevel, ulong sourceLevelId, bool authorized)
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
                    if (role.OrganisationRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.OrganisationId == sourceLevelId))
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
                    if (role.OrganisationRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.OrganisationId == sourceLevelId))
                    {
                        authorized = true;
                        break;
                    }
                    if (role.CompanyRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.CompanyId == sourceLevelId))
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
                    if (role.OrganisationRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.OrganisationId == sourceLevelId))
                    {
                        authorized = true;
                        break;
                    }
                    if (role.CompanyRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.CompanyId == sourceLevelId))
                    {
                        authorized = true;
                        break;
                    }
                    if (role.DepartmentRoleRights.Any(x => x.Right.AsEnum <= requiredRight && x.DepartmentId == sourceLevelId))
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