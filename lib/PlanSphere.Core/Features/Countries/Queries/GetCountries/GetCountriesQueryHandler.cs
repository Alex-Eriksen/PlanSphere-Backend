using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Countries.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Countries.Queries.GetCountries;

[HandlerType(HandlerType.SystemApi)]
public class GetCountriesQueryHandler(
    ICountryRepository countryRepository, 
    Mapper mapper,
    ILogger<GetCountriesQueryHandler> logger
) : IRequestHandler<GetCountriesQuery, List<CountryDTO>>
{
    private readonly ICountryRepository _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
    private readonly Mapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<GetCountriesQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<List<CountryDTO>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("GetCountriesQueryHandler");
        _logger.LogInformation("GetCountriesQueryHandler");
        
        var countries = await _countryRepository.GetCountryLookUps(cancellationToken);
        
        _logger.LogInformation("GetCountriesQueryHandler");
        
        var countryDTOs = _mapper.Map<List<Country>, List<CountryDTO>>(countries);
        
        return countryDTOs;
    }
}