using FluentValidation;

namespace PlanSphere.Core.Features.Users.Queries.GetLoggedInUser;

public class GetLoggedInUserQueryValidator : AbstractValidator<GetLoggedInUserQuery>
{
    public GetLoggedInUserQueryValidator()
    {
        RuleFor(x => x.ClaimedUserId)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.RefreshToken)
            .NotNull()
            .NotEmpty();
    }
}