using MediatR;
using PlanSphere.Core.Features.Companies.DTOs;

namespace PlanSphere.Core.Features.Companies.Queries.LookUp;

public record LookUpCompaniesQuery(ulong OrganisationId, ulong UserId) : IRequest<List<CompanyLookUpDTO>>;
