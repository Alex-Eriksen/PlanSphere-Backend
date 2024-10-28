using FluentValidation;

namespace PlanSphere.Core.Features.Addresses.Requests;

public class AddressRequestValidator : AbstractValidator<AddressRequest>
{
    public AddressRequestValidator()
    {
        RuleFor(command => command.ParrentId).NotEmpty().WithMessage("ParrentId is required");
        RuleFor(command => command.CountryId).NotEmpty().WithMessage("CountryId is required");
        RuleFor(command => command.StreetName).NotEmpty().WithMessage("Street name is required");
        RuleFor(command => command.HouseNumber).NotEmpty().WithMessage("House number is required");
        RuleFor(command => command.PostalCode).NotEmpty().WithMessage("Postal Code is required");
        RuleFor(command => command.Door).NotEmpty().WithMessage("Door is required");
        RuleFor(command => command.Floor).NotEmpty().WithMessage("Floor Code is required");
    }
}