using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Countries.DTOs;
using PlanSphere.Core.Features.Countries.Queries.GetCountries;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Countries.Queries.GetCountryLookUps;

[HandlerType(HandlerType.SystemApi)]
public class GetCountryLookUpsQueryHandler(
    ICountryRepository countryRepository, 
    IMapper mapper,
    ILogger<GetCountryLookUpsQueryHandler> logger
) : IRequestHandler<GetCountryLookUpsQuery, List<CountryLookUpDTO>>
{
    private readonly ICountryRepository _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<GetCountryLookUpsQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<List<CountryLookUpDTO>> Handle(GetCountryLookUpsQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching country lookups");
        
        _logger.LogInformation("Fetching all countries in the database");
        var countries = await _countryRepository.GetCountryLookUpsAsync(cancellationToken);
        _logger.LogInformation("Fetched all countries in the database");

        var countryDTOs = countries.Select(x => _mapper.Map<CountryLookUpDTO>(x)).ToList();
        return countryDTOs;
    }
}