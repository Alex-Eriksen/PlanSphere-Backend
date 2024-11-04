using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Commands.PatchOrganisation;

public record PatchOrganisationCommand(JsonPatchDocument<OrganisationUpdateRequest> PatchDocument) : IRequest
{
    [JsonIgnore] 
    public ulong Id { get; set; }
}