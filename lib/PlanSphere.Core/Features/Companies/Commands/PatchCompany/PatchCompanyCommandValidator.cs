using FluentValidation;

namespace PlanSphere.Core.Features.Companies.Commands.PatchCompany;

public class PatchCompanyCommandValidator : AbstractValidator<PatchCompanyCommand>
{
    public PatchCompanyCommandValidator()
    {
        RuleFor(x => x.PatchDocument)
            .NotNull();
        RuleFor(x => x.Id)
            .NotNull();
    }
}