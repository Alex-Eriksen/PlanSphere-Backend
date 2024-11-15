using PlanSphere.Core.Constants;
using PlanSphere.Core.Interfaces.ActionFilters.LateFilters;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Filters.LateFilters;

public class OrganisationFilter(IOrganisationRepository organisationRepository) : IOrganisationFilter
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));

    public async Task<bool> CheckIsOrganisationOwnerAsync(ulong organisationId, ulong userId, CancellationToken cancellationToken, bool shouldThrow = true)
    {
        var organisation = await _organisationRepository.GetByIdAsync(organisationId, cancellationToken);

        var isOwner = organisation.OwnerId == userId;

        if(shouldThrow && !isOwner) throw new UnauthorizedAccessException(ErrorMessageConstants.UnauthorizedActionMessage);

        return isOwner;
    }
}