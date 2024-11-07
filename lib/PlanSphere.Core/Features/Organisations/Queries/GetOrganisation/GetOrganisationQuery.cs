using Domain.Entities.EmbeddedEntities;
using MediatR;
using PlanSphere.Core.Features.Organisations.DTOs;

namespace PlanSphere.Core.Features.Organisations.Queries.GetOrganisation;

public record GetOrganisationQuery(ulong SourceLevelId) : IRequest<OrganisationDetailDTO>;
