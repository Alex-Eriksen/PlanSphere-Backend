using System.Text;
using Domain.Entities.EmbeddedEntities;
using Microsoft.AspNetCore.Mvc.Filters;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Exceptions;
using PlanSphere.Core.Extensions.HttpContextExtensions;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.SystemApi.Extensions;

namespace PlanSphere.SystemApi.Action_Filters;

public class RoleActionFilter(
    IUserRepository userRepository,
    Right requiredRight
) : ActionFilterAttribute
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = await _userRepository.GetByIdAsync(context.HttpContext.User.GetUserId(), CancellationToken.None);
        var userRoles = user.Roles.Select(x => x.Role).ToList();
        
        // The id of the source level entity (read from the request route), e.x.: Company id
        var sourceLevelId = context.HttpContext.GetSourceLevelId();
        
        // Source level of the request (read from the request body).
        var sourceLevel = await context.HttpContext.GetSourceLevelAsync();

        var authorized = false;

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
                    if (role.CompanyRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.CompanyId == sourceLevelId) == null)
                    {
                        continue;
                    }

                    authorized = true;
                    break;
                case SourceLevel.Department:
                    if (role.DepartmentRoleRights.SingleOrDefault(x => x.Right.AsEnum == requiredRight && x.DepartmentId == sourceLevelId) == null)
                    {
                        continue;
                    }

                    authorized = true;
                    break;
                case SourceLevel.Team:
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

        if (!authorized)
        {
            throw new ForbiddenAccessException("You are not authorized to perform this action!");
        }

        await next();
    }
}