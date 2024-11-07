using System.Security.Claims;
using PlanSphere.Core.Constants;

namespace PlanSphere.SystemApi.Extensions;

public static class ClaimsExtensions
{
    public static ulong GetOrganisationId(this ClaimsPrincipal context)
    {
        var organizationIdClaim = context.FindFirst(ClaimsConstants.OrganisationId)?.Value;
        ulong.TryParse(organizationIdClaim, out var organizationId);

        return organizationId;
    }
    
    public static ulong GetUserId(this ClaimsPrincipal context)
    {
        var userIdClaim = context.FindFirst(ClaimsConstants.UserId)?.Value;
        ulong.TryParse(userIdClaim, out var userId);

        return userId;
    }
    
    public static ulong GetEmail(this ClaimsPrincipal context)
    {
        var emailClaim = context.FindFirst(ClaimsConstants.Email)?.Value;
        ulong.TryParse(emailClaim, out var email);

        return email;
    }
    
    public static ulong GetFirstName(this ClaimsPrincipal context)
    {
        var firstNameClaim = context.FindFirst(ClaimsConstants.FirstName)?.Value;
        ulong.TryParse(firstNameClaim, out var firstName);

        return firstName;
    }
    
    public static ulong GetLastName(this ClaimsPrincipal context)
    {
        var lastNameClaim = context.FindFirst(ClaimsConstants.LastName)?.Value;
        ulong.TryParse(lastNameClaim, out var lastName);

        return lastName;
    }
}