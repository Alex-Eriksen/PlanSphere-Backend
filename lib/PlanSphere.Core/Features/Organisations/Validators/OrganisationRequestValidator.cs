using FluentValidation;
using PlanSphere.Core.Features.Companies.Validators;
using PlanSphere.Core.Features.JobTitles.Validators;
using PlanSphere.Core.Features.Organisations.Requests;
using PlanSphere.Core.Features.Roles.Validators;

namespace PlanSphere.Core.Features.Organisations.Validators;

public class OrganisationRequestValidator : AbstractValidator<OrganisationRequest>
{
    private readonly CompanyRequestValidator _companyRequestValidator;
    private readonly JobTitleRequestValidator _jobTitleRequestValidator;
    private readonly RoleRequestValidator _roleRequestValidator;
    public OrganisationRequestValidator(CompanyRequestValidator companyRequestValidator, JobTitleRequestValidator jobTitleRequestValidator, RoleRequestValidator roleRequestValidator)
    {
        _companyRequestValidator = companyRequestValidator ?? throw new ArgumentNullException(nameof(companyRequestValidator));
        _jobTitleRequestValidator = jobTitleRequestValidator ?? throw new ArgumentNullException(nameof(jobTitleRequestValidator));
        _roleRequestValidator = roleRequestValidator ?? throw new ArgumentNullException(nameof(roleRequestValidator));
        
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.MemberIds).NotNull();
        RuleForEach(x => x.CompanyRequests).SetValidator(_companyRequestValidator);
        RuleForEach(x => x.JobTitleRequests).SetValidator(_jobTitleRequestValidator);
        RuleForEach(x => x.RoleRequests).SetValidator(_roleRequestValidator);
    }
}