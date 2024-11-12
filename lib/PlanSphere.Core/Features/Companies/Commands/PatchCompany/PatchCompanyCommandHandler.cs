using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Companies.Request;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Companies.Commands.PatchCompany;
[HandlerType(HandlerType.SystemApi)]
public class PatchCompanyCommandHandler(
    ICompanyRepository companyRepository,
    ILogger<PatchCompanyCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<PatchCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ILogger<PatchCompanyCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task Handle(PatchCompanyCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching company");
        _logger.LogInformation("Fetching company with id: [{companyId}]", command.Id);
        var company = await _companyRepository.GetByIdAsync(command.Id, cancellationToken);
        _logger.LogInformation("Fetched company with id: [{companyId}]", command.Id);
        
        var companyPatchRequest = _mapper.Map<CompanyUpdateRequest>(company);
        
        command.PatchDocument.ApplyTo(companyPatchRequest);
        
        company = _mapper.Map(companyPatchRequest, company);

        if (company.Settings.InheritDefaultWorkSchedule)
        {
            company.Settings.DefaultWorkSchedule.ParentId = company.Organisation.Settings.DefaultWorkScheduleId;
        }
        else
        {
            company.Settings.DefaultWorkSchedule.Parent = null;
            company.Settings.DefaultWorkSchedule.ParentId = null;
        }
        
        _logger.LogInformation("Patching Company with id: [{companyId}]", command.Id);
        await _companyRepository.UpdateAsync(command.Id, company, cancellationToken);
        _logger.LogInformation("Patched Company with id: [{companyId}]", command.Id);
    }
}