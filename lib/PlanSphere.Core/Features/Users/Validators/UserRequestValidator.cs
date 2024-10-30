using FluentValidation;
using PlanSphere.Core.Features.Address.Requests;
using PlanSphere.Core.Features.Users.Requests;

namespace PlanSphere.Core.Features.Users.Validators;

public class UserRequestValidator : AbstractValidator<UserRequest>
{
    private readonly AddressRequestValidator _validator;
    public UserRequestValidator(AddressRequestValidator validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.FirstName)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.LastName)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Address)
            .SetValidator(_validator);
    }
}