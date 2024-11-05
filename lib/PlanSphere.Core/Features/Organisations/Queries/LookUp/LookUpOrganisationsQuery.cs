using MediatR;
using PlanSphere.Core.Features.Organisations.DTOs;

namespace PlanSphere.Core.Features.Organisations.Queries.LookUp;

public record LookUpOrganisationsQuery(ulong UserId) : IRequest<List<OrganisationLookUpDTO>>;
