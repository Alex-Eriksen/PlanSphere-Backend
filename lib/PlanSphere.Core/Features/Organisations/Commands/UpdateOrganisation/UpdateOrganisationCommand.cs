using MediatR;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Commands.UpdateOrganisation;

public record UpdateOrganisationCommand(OrganisationUpdateRequest OrganisationUpdateRequest) : IRequest
{
    public ulong OrganisationId { get; set; }
}