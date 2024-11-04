using FluentValidation;

namespace PlanSphere.Core.Features.Companies.Commands.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(x => x.Request.CompanyName)
            .NotEmpty();
    }
}