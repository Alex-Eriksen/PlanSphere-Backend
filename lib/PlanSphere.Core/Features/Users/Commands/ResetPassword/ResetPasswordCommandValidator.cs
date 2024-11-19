using FluentValidation;

namespace PlanSphere.Core.Features.Users.Commands.ResetPassword;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.EmailVerificationToken).NotNull();
        RuleFor(x => x.ResetPasswordVerificationToken).NotNull();
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.Password).NotNull().MinimumLength(8);
        RuleFor(x => x.Password).Must(password => password.Any(c => !char.IsLetterOrDigit(c))).WithMessage("Password must contain at least one special character");
        RuleFor(x => x.Password).Must(password => password.Any(char.IsDigit)).WithMessage("Password must contain at least one digit");
        RuleFor(x => x.Password).Must(password => password.Any(char.IsUpper)).WithMessage("Password must contain at least one uppercase letter");
        RuleFor(x => x.Password).Must(password => password.Any(char.IsLower)).WithMessage("Password must contain at least one lowercase letter");
    }
}