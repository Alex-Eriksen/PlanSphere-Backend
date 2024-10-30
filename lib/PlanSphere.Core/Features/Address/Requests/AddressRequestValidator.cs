using FluentValidation;

namespace PlanSphere.Core.Features.Address.Requests;

public class AddressRequestValidator : AbstractValidator<AddressRequest>
{
    public AddressRequestValidator()
    {
        RuleFor(x => x.StreetName)
            .NotNull()
            .NotEmpty();
        
        RuleFor(x => x.HouseNumber)
            .NotNull()
            .NotEmpty();
    }
}