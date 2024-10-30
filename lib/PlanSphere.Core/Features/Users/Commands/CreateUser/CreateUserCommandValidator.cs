using FluentValidation;
using PlanSphere.Core.Features.Users.Validators;

namespace PlanSphere.Core.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    private readonly UserRequestValidator _validator;
    public CreateUserCommandValidator(UserRequestValidator validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));

        RuleFor(x => x.OrganisationId)
            .NotNull();

        RuleFor(x => x.Request)
            .SetValidator(_validator);
    }
}