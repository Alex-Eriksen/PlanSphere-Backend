using FluentValidation;
using PlanSphere.Core.Constants;

namespace PlanSphere.Core.Features.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.OrganisationId).NotNull();
        RuleFor(x => x.SourceLevel).NotNull().IsInEnum();
        RuleFor(x => x.SourceLevelId).NotNull();
        RuleFor(x => x.UserRequest.FirstName).NotNull().MaximumLength(FieldLengthConstants.Medium);
        RuleFor(x => x.UserRequest.LastName).NotNull().MaximumLength(FieldLengthConstants.Short);
        RuleFor(x => x.UserRequest.Email).NotNull().EmailAddress().MaximumLength(FieldLengthConstants.Medium);
        RuleFor(x => x.UserRequest.Password).NotNull();
        RuleFor(x => x.UserRequest.Address).NotNull();
    }
}