using FluentValidation;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Validators;

public class OrganisationRequestValidator : AbstractValidator<OrganisationRequest>
{
    private readonly CompanyRequestValidator _companyRequestValidator;
    private readonly DepartmentRequestValidator _departmentRequestValidator;
    private readonly TeamRequestValidator _teamRequestValidator;
    public OrganisationRequestValidator(CompanyRequestValiadtor companyRequestValidator, DepartmentRequestValidator departmentRequestValidator, TeamRequestValidator teamRequestValidator)
    {
        _companyRequestValiadtor = companyRequestValidator ?? trow new ArgumentNullException(nameof(companyRequestValidator));
        _departmentRequestValiadtor = departmentRequestValidator ?? trow new ArgumentNullException(nameof(departmentRequestValidator));
        _teamRequestValiadtor = teamRequestValidator ?? trow new ArgumentNullException(nameof(teamRequestValidator));
        
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.MemberIds).NotNull();
        RuleFor(x => x.CompanyRequests).SetValidator(_companyRequestValidator);
        RuleFor(x => x.JobTitleRequests).SetValidator(_departmentRequestValiadtor);
        RuleFor(x => x.RoleRequests).SetValidator(_teamRequestValiadtor);
    }
}