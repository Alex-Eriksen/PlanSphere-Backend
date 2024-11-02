using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Organisations.Commands.DeleteOrganisation;
[HandlerType(HandlerType.SystemApi)]
public class DeleteOrganisationCommandHandler(
    IOrganisationRepository organisationRepository,
    ILogger<DeleteOrganisationCommandHandler> logger
) : IRequestHandler<DeleteOrganisationCommand>
{
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly ILogger<DeleteOrganisationCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    public async Task Handle(DeleteOrganisationCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Starting to delete organistion with DeleteOrganisationCommandHandler with id: [{organisationId}]",request.OrganisationId);
        _logger.LogInformation("Deleting organisation with id: [{id}]", request.OrganisationId);
        await _organisationRepository.DeleteAsync(request.OrganisationId, cancellationToken);
        _logger.LogInformation("Deleted organisation with id: [{id}]", request.OrganisationId);
    }
}