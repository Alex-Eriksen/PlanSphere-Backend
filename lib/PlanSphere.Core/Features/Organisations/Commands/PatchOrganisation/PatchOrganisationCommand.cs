using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Commands.PatchOrganisation;

public record PatchOrganisationCommand(JsonPatchDocument<OrganisationRequest> PatchDocument,  ulong OrganisationId) : IRequest;
