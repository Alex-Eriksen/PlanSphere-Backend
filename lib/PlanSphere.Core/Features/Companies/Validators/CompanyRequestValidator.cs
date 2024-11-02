using FluentValidation;
using PlanSphere.Core.Features.Addresses.Requests;
using PlanSphere.Core.Features.Companies.Request;

namespace PlanSphere.Core.Features.Companies.Validators;

public class CompanyRequestValidator : AbstractValidator<CompanyRequest>
{
    public CompanyRequestValidator()
    {
        RuleFor(x => x.CompanyName).NotNull();
        RuleFor(x => x.CVR).NotNull();
        RuleFor(x => x.CVR).NotNull();
    }
}