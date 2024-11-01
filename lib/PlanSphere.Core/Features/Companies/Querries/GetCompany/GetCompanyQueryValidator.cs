using FluentValidation;

namespace PlanSphere.Core.Features.Companies.Qurries.GetCompany;

    public class GetCompanyQueryValidator : AbstractValidator<GetCompanyQuery>
    {
        public GetCompanyQueryValidator() 
        {
            RuleFor(x => x.Id)
                .NotNull();
        }
    }
