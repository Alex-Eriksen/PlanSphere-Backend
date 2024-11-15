using System.Text.Json.Serialization;
using MediatR;
using PlanSphere.Core.Features.Companies.Request;

namespace PlanSphere.Core.Features.Companies.Commands.CreateCompany;
public record CreateCompanyCommand (CompanyRequest Request) : IRequest
{
    [JsonIgnore]
    public ulong OrganisationId { get; set; }
    public bool InheritAddress { get; set; }
}