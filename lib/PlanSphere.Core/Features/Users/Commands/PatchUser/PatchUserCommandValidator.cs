using FluentValidation;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Extensions.ValidationExtensions;
using PlanSphere.Core.Features.Users.Requests;

namespace PlanSphere.Core.Features.Users.Commands.PatchUser;

public class PatchUserCommandValidator : AbstractValidator<PatchUserCommand>
{
    public PatchUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull();

        RuleForEach(x => x.Request.Operations)
            .AllowOperators(PatchOperators.REPLACE)
            .NotNull(nameof(UserPatchRequest.FirstName))
            .NotNull(nameof(UserPatchRequest.LastName))
            .NotNull(nameof(UserPatchRequest.Email));
    }
}