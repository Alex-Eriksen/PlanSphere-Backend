namespace PlanSphere.Core.Interfaces.ActionFilters.LateFilters;

public interface IOrganisationFilter
{
    Task CheckIsOrganisationOwnerAsync(ulong organisationId, ulong userId, CancellationToken cancellationToken);
}