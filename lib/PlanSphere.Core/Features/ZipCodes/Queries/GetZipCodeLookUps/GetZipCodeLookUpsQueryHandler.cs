using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.ZipCodes.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.ZipCodes.Queries.GetZipCodeLookUps;

[HandlerType(HandlerType.SystemApi)]
public class GetZipCodeLookUpsQueryHandler(
    IZipCodeRepository zipCodeRepository,
    IMapper mapper,
    ILogger<GetZipCodeLookUpsQueryHandler> logger
) : IRequestHandler<GetZipCodeLookUpsQuery, List<ZipCodeLookUpDTO>>
{
    private readonly IZipCodeRepository _zipCodeRepository = zipCodeRepository ?? throw new ArgumentNullException(nameof(zipCodeRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly ILogger<GetZipCodeLookUpsQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    public async Task<List<ZipCodeLookUpDTO>> Handle(GetZipCodeLookUpsQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Starting to List zipcodes with [GetZipCodeLookUpsQueryHandler]");
        
        _logger.LogInformation("Fetching all zipcodes in the database");
        var zipcodes = await _zipCodeRepository.GetZipCodeLookUpsAsync(cancellationToken);
        _logger.LogInformation("Fetched all zipcodes in the database");

        var zipcodeDTOs = _mapper.Map<List<ZipCodeLookUpDTO>>(zipcodes);
        return zipcodeDTOs;
    }
}