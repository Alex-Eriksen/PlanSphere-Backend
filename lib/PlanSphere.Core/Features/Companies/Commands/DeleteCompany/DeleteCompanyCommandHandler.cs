using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;

namespace PlanSphere.Core.Features.Companies.Commands.DeleteCompany;
[HandlerType(HandlerType.SystemApi)]
public class DeleteCompanyCommandHandler(
    ICompanyRepository companyRepository,
    ILogger<DeleteCompanyCommandHandler> logger
) : IRequestHandler<DeleteCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ILogger<DeleteCompanyCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
    
    public async Task Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Deleting Company");
        _logger.LogInformation("Deleting Company with id [{id}]", request.Id);
        await _companyRepository.DeleteAsync(request.Id, cancellationToken);
        _logger.LogInformation("Deleted Company with id [{id}]!", request.Id);
    }
}