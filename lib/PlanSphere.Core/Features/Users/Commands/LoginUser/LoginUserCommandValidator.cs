using FluentValidation;

namespace PlanSphere.Core.Features.Users.Commands.LoginUser;

public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.Password)
            .NotNull()
            .NotEmpty();
    }
}