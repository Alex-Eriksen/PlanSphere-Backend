using FluentValidation;

namespace PlanSphere.Core.Features.Companies.Commands.DeleteCompany;

public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
{
    public DeleteCompanyCommandValidator()
    {
        RuleFor(x => x.Id).NotNull();
    }
}