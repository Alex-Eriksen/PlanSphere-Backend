using MediatR;
using PlanSphere.Core.Features.Companies.Request;

namespace PlanSphere.Core.Features.Companies.Commands.CreateCompany;
public record CreateCompanyCommand (CompanyRequest Request) : IRequest
{
    public ulong OrganisationId { get; set; }
    
}