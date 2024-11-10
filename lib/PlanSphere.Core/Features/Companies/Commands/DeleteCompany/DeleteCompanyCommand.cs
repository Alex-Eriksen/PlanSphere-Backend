using MediatR;

namespace PlanSphere.Core.Features.Companies.Commands.DeleteCompany;

    public record DeleteCompanyCommand (ulong Id) : IRequest;