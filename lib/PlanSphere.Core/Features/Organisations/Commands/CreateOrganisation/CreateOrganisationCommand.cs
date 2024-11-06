using Domain.Entities.EmbeddedEntities;
using MediatR;
using Newtonsoft.Json;
using PlanSphere.Core.Features.Addresses.Requests;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;

public record CreateOrganisationCommand(OrganisationRequest Request) : IRequest
{
    [JsonIgnore]
    public ulong SourceLevelId { get; set; }
    [JsonIgnore]
    public SourceLevel SourceLevel { get; set; }
}