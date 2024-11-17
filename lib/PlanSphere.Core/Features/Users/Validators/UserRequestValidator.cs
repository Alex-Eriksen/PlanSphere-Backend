using FluentValidation;
using PlanSphere.Core.Features.Users.Requests;

namespace PlanSphere.Core.Features.Users.Validators;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    public UserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty();
    }
}