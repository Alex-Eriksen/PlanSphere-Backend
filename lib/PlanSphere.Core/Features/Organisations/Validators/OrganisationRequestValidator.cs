using FluentValidation;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Validators;

public class OrganisationRequestValidator : AbstractValidator<OrganisationRequest>
{
    public OrganisationRequestValidator()
    {
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.MemberIds).NotNull();
        RuleFor(x => x.CompanyRequests).NotNull();
        RuleFor(x => x.JobTitleRequests).NotNull();
        RuleFor(x => x.RoleRequests).NotNull();
    }
}