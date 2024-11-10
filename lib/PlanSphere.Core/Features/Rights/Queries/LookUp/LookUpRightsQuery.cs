using MediatR;
using PlanSphere.Core.Features.Rights.DTOs;

namespace PlanSphere.Core.Features.Rights.Queries.LookUp;

public record LookUpRightsQuery : IRequest<List<RightLookUpDTO>>;
