using PlanSphere.Core.Constants;
using PlanSphere.Core.Interfaces.ActionFilters.LateFilters;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Filters.LateFilters;

public class OrganisationFilter(IOrganisationRepository organisationRepository) : IOrganisationFilter
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));

    public async Task CheckIsOrganisationOwnerAsync(ulong organisationId, ulong userId, CancellationToken cancellationToken)
    {
        var organisation = await _organisationRepository.GetByIdAsync(organisationId, cancellationToken);

        if (organisation.OwnerId == userId) return;

        throw new UnauthorizedAccessException(ErrorMessageConstants.UnauthorizedActionMessage);
    }
}