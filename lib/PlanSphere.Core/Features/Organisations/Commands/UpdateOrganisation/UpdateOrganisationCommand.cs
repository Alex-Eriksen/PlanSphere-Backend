using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Commands.UpdateOrganisation;

public record UpdateOrganisationCommand(OrganisationRequest OrganisationRequest) : IRequest
{
    [JsonIgnore]
    public ulong SourceLevelId { get; set; }
    [JsonIgnore]
    public SourceLevel SourceLevel { get; set; }
    public ulong OrganisationId { get; set; }
}