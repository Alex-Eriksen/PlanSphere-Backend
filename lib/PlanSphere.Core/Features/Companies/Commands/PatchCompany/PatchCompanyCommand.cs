using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using PlanSphere.Core.Features.Companies.Request;

namespace PlanSphere.Core.Features.Companies.Commands.PatchCompany;

public record PatchCompanyCommand(JsonPatchDocument<CompanyUpdateRequest> PatchDocument) : IRequest
{
    [JsonIgnore]
    public ulong Id { get; set; }
}