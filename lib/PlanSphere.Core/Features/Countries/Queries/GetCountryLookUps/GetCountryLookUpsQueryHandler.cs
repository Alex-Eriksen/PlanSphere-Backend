using AutoMapper;
using Domain.Entities;
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
    Mapper mapper,
    ILogger<GetCountryLookUpsQueryHandler> logger
) : IRequestHandler<GetCountryLookUpsQuery, List<CountryLookUpDTO>>
{
    private readonly ICountryRepository _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
    private readonly Mapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<GetCountryLookUpsQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<List<CountryLookUpDTO>> Handle(GetCountryLookUpsQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("GetCountriesQueryHandler");
        _logger.LogInformation("GetCountriesQueryHandler");
        
        var countries = await _countryRepository.GetCountryLookUpsAsync(cancellationToken);
        
        _logger.LogInformation("GetCountriesQueryHandler");
        
        var countryDTOs = _mapper.Map<List<Country>, List<CountryLookUpDTO>>(countries);
        
        return countryDTOs;
    }
}