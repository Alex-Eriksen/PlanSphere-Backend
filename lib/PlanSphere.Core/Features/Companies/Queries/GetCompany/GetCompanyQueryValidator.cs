using FluentValidation;

namespace PlanSphere.Core.Features.Companies.Queries.GetCompany;

public class GetCompanyQueryValidator : AbstractValidator<GetCompanyQuery>
{
    public GetCompanyQueryValidator() 
    {
        RuleFor(x => x.Id)
            .NotNull();
    }
}
