using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanSphere.Core.Features.Companies.Qurries.ListCompanies;

public class ListCompaniesQueryValidator : AbstractValidator<ListCompaniesQuery>
{
    public ListCompaniesQueryValidator()
    {
        RuleFor(x => x.OrganisationId)
            .NotNull();

        RuleFor(x => x.SortBy)
            .IsInEnum()
            .NotNull();

        RuleFor(x => x.SortDescending)
            .NotNull();

        RuleFor(x => x.PageNumber)
            .NotNull();

        RuleFor(x => x.PageSize)
            .NotNull();
    }
}