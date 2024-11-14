using FluentValidation;
using PlanSphere.Core.Features.Users.Validators;

namespace PlanSphere.Core.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly UserRequestValidator _validator;

    public UpdateUserCommandValidator(UserRequestValidator validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));

        RuleFor(x => x.UserId)
            .NotNull();

        RuleFor(x => x.Request.FirstName)
            .NotNull();
        
        RuleFor(x => x.Request.LastName)
            .NotNull();
        
        RuleFor(x => x.Request.Email)
            .NotNull();
        
        RuleFor(x => x.Request.PhoneNumber)
            .NotNull();
    }
}