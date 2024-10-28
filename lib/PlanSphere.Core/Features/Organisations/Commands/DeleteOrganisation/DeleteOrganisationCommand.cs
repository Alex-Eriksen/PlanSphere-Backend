using MediatR;

namespace PlanSphere.Core.Features.Organisations.Commands.DeleteOrganisation;

public record DeleteOrganisationCommand(ulong Id) : IRequest;  