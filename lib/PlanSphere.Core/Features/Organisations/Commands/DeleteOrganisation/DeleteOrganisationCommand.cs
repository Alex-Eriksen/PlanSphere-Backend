using Domain.Entities.EmbeddedEntities;
using MediatR;
using Newtonsoft.Json;

namespace PlanSphere.Core.Features.Organisations.Commands.DeleteOrganisation;

public record DeleteOrganisationCommand(ulong OrganisationId) : IRequest;
