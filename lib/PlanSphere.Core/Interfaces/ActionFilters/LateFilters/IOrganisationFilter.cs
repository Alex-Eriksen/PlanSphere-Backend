namespace PlanSphere.Core.Interfaces.ActionFilters.LateFilters;

public interface IOrganisationFilter
{
    Task<bool> CheckIsOrganisationOwnerAsync(ulong organisationId, ulong userId, CancellationToken cancellationToken, bool shouldThrow = true);
}