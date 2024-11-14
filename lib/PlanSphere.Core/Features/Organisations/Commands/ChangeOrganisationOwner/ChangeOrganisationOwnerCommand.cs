using MediatR;

namespace PlanSphere.Core.Features.Organisations.Commands.ChangeOrganisationOwner;

public record ChangeOrganisationOwnerCommand(ulong UserId, ulong OrganisationId) : IRequest;
