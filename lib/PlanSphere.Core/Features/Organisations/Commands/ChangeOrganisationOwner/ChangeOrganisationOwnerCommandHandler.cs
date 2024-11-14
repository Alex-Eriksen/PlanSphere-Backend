using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Organisations.Commands.ChangeOrganisationOwner;

[HandlerType(HandlerType.SystemApi)]
public class ChangeOrganisationOwnerCommandHandler(
    ILogger<ChangeOrganisationOwnerCommandHandler> logger,
    IOrganisationRepository organisationRepository
) : IRequestHandler<ChangeOrganisationOwnerCommand>
{
    private readonly ILogger<ChangeOrganisationOwnerCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));

    public async Task Handle(ChangeOrganisationOwnerCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Change organisation owner");
        _logger.LogInformation("Changing organisation owner of organisation with id: [{organisationId}] to user with id: [{userId}]", request.OrganisationId, request.UserId);
        var organisation = await _organisationRepository.GetByIdAsync(request.OrganisationId, cancellationToken);
        organisation.OwnerId = request.UserId;
        await _organisationRepository.UpdateAsync(organisation.Id, organisation, cancellationToken);
        _logger.LogInformation("Changed organisation owner of organisation with id: [{organisationId}] to user with id: [{userId}]", request.OrganisationId, request.UserId);
    }
}