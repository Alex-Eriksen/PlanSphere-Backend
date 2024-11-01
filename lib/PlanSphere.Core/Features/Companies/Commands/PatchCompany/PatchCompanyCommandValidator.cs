using FluentValidation;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Extensions.ValidationExtensions;
using PlanSphere.Core.Features.Companies.Request;

namespace PlanSphere.Core.Features.Companies.Commands.PatchCompany;

public class PatchCompanyCommandValidator : AbstractValidator<PatchCompanyCommand>
{
    public PatchCompanyCommandValidator()
    {
        RuleFor(x => x.PatchDocument)
            .NotNull();
        RuleFor(x => x.Id)
            .NotNull();
        
        RuleForEach(x => x.PatchDocument.Operations)
            .AllowOperators(PatchOperators.REPLACE)
            .NotNull(nameof(CompanyUpdateRequest.CompanyName))
            .NotNull(nameof(CompanyUpdateRequest.CVR));
        

    }
}