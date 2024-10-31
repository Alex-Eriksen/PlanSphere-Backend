using FluentValidation;

namespace PlanSphere.Core.Features.JobTitles.Queries.GetJobTitle;

public class GetJobTitleQueryValidator : AbstractValidator<GetJobTitleQuery>
{
    public GetJobTitleQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull();
    }
}