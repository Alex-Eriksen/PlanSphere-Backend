using MediatR;
using PlanSphere.Core.Features.Countries.DTOs;

namespace PlanSphere.Core.Features.Countries.Queries.GetCountries;

public record GetCountryLookUpsQuery : IRequest<List<CountryLookUpDTO>>;