using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Companies.DTOs;
using PlanSphere.Core.Interfaces.Repositories;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Features.Companies.Queries.LookUp;

[HandlerType(HandlerType.SystemApi)]
public class LookUpCompaniesQueryHandler(
    ILogger<LookUpCompaniesQueryHandler> logger,
    IMapper mapper,
    ICompanyRepository companyRepository,
    IUserRepository userRepository
) : IRequestHandler<LookUpCompaniesQuery, List<CompanyLookUpDTO>>
{
    private readonly ILogger<LookUpCompaniesQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));

    public async Task<List<CompanyLookUpDTO>> Handle(LookUpCompaniesQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Looking up companies.");
        _logger.LogInformation("Retrieving companies in organisation: {organisationId}", request.OrganisationId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        var companies = await _companyRepository
            .GetQueryable()
            .Where(x => x.OrganisationId == request.OrganisationId)
            .ToListAsync(cancellationToken);
        _logger.LogInformation("Retrieved companies in organisation: {organisationId}", request.OrganisationId);

        var companiesToAdd = new List<Company>();
        
        foreach (var role in user.Roles.Select(x => x.Role))
        {
            if (role.OrganisationRoleRights.Where(x => x.Right.AsEnum <= Right.View).ToList().Count > 0)
            {
                companiesToAdd = companies;
                break;
            }
            
            companiesToAdd.AddRange(companies.Where(x => 
                role.CompanyRoleRights
                    .Where(z => z.Right.AsEnum <= Right.View)
                    .Select(y => y.CompanyId)
                    .Contains(x.Id)
                ).ToList());
        }

        companiesToAdd = companiesToAdd.Distinct().ToList();
        
        var companyLookUpDtos = _mapper.Map<List<CompanyLookUpDTO>>(companiesToAdd);

        return companyLookUpDtos;
    }
}