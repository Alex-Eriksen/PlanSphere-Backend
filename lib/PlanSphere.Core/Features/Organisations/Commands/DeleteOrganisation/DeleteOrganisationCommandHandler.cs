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
        logger.BeginScope("DeleteOrganisationCommandHandler");
        
        _logger.LogInformation("Deleting organisation with id: [{id}]", request.Id);
        await _organisationRepository.DeleteAsync(request.Id, cancellationToken);
        _logger.LogInformation("Deleted organisation with id: [{id}]", request.Id);
    }
}