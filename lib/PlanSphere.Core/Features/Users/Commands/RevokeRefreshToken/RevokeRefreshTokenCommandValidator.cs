using FluentValidation;

namespace PlanSphere.Core.Features.Users.Commands.RevokeRefreshToken;

public class RevokeRefreshTokenCommandValidator : AbstractValidator<RevokeRefreshTokenCommand>
{
    public RevokeRefreshTokenCommandValidator()
    {
        RuleFor(x => x.Token).NotNull();
        RuleFor(x => x.IpAddress).NotNull();
    }
}