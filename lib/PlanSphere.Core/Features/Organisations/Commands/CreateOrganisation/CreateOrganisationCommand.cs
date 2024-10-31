using MediatR;
using PlanSphere.Core.Features.Address.Requests;

namespace PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;

public record CreateOrganisationCommand(string Name, AddressRequest Address) : IRequest;