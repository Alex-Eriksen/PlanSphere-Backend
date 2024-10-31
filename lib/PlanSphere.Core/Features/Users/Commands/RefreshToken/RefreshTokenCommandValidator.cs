using FluentValidation;

namespace PlanSphere.Core.Features.Users.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.Token).NotNull();
        RuleFor(x => x.IpAddress).NotNull();
    }
}