using MediatR;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;

public record CreateOrganisationCommand(OrganisationRequest Request) : IRequest;