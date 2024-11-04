using MediatR;
using PlanSphere.Core.Features.Organisations.DTOs;

namespace PlanSphere.Core.Features.Organisations.Queries.GetOrganisationDetails;

public record GetOrganisationDetailsQuery(ulong Id) : IRequest<OrganisationDetailDTO>;
