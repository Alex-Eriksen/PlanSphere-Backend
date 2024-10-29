using MediatR;

namespace PlanSphere.Core.Features.Organisations.Commands.GetOrganisation;

public record GetOrganisationCommand(ulong Id) : IRequest;