using MediatR;

namespace PlanSphere.Core.Features.Organisations.Commands.DeleteOrganisation;

public record DeleteOrganisationCommand() : IRequest
{
    public ulong Id { get; set; }
} 