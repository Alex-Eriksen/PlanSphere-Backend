using MediatR;
using PlanSphere.Core.Features.ZipCodes.DTOs;

namespace PlanSphere.Core.Features.ZipCodes.Queries.GetZipCodeLookUps;

public record GetZipCodeLookUpsQuery : IRequest<List<ZipCodeLookUpDTO>>;