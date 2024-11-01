using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanSphere.Core.Features.Companies.Commands.UpdateCompany;

[HandlerType(HandlerType.SystemApi)]
public class UpdateCompanyCommandHandler(
    ICompanyRepository companyRepository,
    ILogger<UpdateCompanyCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<UpdateCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ILogger<UpdateCompanyCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(UpdateCompanyCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching company with id: [{companyId}]", command.Id);
        var company = await _companyRepository.GetByIdAsync(command.Id, cancellationToken);
        _logger.LogInformation("Fetched company with id: [{companyId}]", command.Id);

        var mappedCompany = _mapper.Map(command, company);

        _logger.LogInformation("Updating Company with id: [{companyId}]", command.Id);
        await _companyRepository.UpdateAsync(command.Id, mappedCompany, cancellationToken);
        _logger.LogInformation("Updated Company with id: [{companyId}]", command.Id);
    }
}