using MediatR;
using PlanSphere.Core.Features.Companies.DTOs;

namespace PlanSphere.Core.Features.Companies.Queries.GetCompany;

public record GetCompanyQuery (ulong Id) : IRequest<CompanyDTO>;