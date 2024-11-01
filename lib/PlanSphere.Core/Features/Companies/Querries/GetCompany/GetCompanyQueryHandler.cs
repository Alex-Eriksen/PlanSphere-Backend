using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Companies.DTOs;
using PlanSphere.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanSphere.Core.Features.Companies.Qurries.GetCompany;
[HandlerType(HandlerType.SystemApi)]
public class GetCompanyQueryHandler(
    ICompanyRepository companyRepository,
    ILogger<GetCompanyQueryHandler> logger,
    IMapper mapper
) : IRequestHandler<GetCompanyQuery, CompanyDTO>
{
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ILogger<GetCompanyQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(_logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task<CompanyDTO> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        logger.BeginScope("Fetching Company");
        logger.LogInformation("Fetching Company with id: [{Id}]", request.Id);
        var company = await _companyRepository.GetByIdAsync(request.Id, cancellationToken);
        logger.LogInformation("Fetched Company with id: [{Id}]", request.Id);

        var mappedCompany = _mapper.Map<CompanyDTO>(company);
        return mappedCompany;
    }

}