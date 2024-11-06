using FluentValidation;

namespace PlanSphere.Core.Features.Users.Queries.GetUserDetails;

public class GetUserDetailsQueryValidator : AbstractValidator<GetUserDetailsQuery>
{
    public GetUserDetailsQueryValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.SourceLevel).NotNull().IsInEnum();
        RuleFor(x => x.SourceLevelId).NotNull();
    }
}