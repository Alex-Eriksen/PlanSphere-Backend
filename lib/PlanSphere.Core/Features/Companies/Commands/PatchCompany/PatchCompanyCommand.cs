using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using PlanSphere.Core.Features.Companies.Request;

namespace PlanSphere.Core.Features.Companies.Commands.PatchCompany;

public record PatchCompanyCommand(JsonPatchDocument<CompanyUpdateRequest> PatchDocument) : IRequest
{
    public ulong Id { get; set; }
}