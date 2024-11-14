using FluentValidation;

namespace PlanSphere.Core.Features.JobTitles.Queries.LookUpJobTitles;

public class LookUpJobTitlesCommandValidator : AbstractValidator<LookUpJobTitlesCommand>
{
    public LookUpJobTitlesCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull();
    }
}