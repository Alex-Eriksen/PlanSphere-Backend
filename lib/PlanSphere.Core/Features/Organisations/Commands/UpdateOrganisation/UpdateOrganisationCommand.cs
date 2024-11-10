using MediatR;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Commands.UpdateOrganisation;

public record UpdateOrganisationCommand(OrganisationRequest OrganisationRequest, ulong SourceLevelId) : IRequest;
