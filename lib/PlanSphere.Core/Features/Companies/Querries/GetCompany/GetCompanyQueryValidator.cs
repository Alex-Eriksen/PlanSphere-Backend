using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanSphere.Core.Features.Companies.Qurries.GetCompany
{
    public class GetCompanyQueryValidator : AbstractValidator<GetCompanyQuery>
    {
        public GetCompanyQueryValidator() 
        {
            RuleFor(x => x.Id)
                .NotNull();
        }
    }
}