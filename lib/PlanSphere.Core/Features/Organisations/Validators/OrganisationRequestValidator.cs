using Domain.Entities;
using FluentValidation;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Validators;

public class OrganisationRequestValidator : AbstractValidator<OrganisationRequest>
{
    private readonly OrganisationRequestValidator _validator;

    public OrganisationRequestValidator(OrganisationRequestValidator validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));

        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.Members).NotNull();
        RuleFor(x => x.CompanyRequests).NotNull();
        RuleFor(x => x.JobTitleRequests).NotNull();
        RuleFor(x => x.RoleRequests).NotNull();
    }
}