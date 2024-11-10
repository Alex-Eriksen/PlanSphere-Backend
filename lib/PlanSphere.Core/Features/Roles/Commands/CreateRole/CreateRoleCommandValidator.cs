using FluentValidation;
using PlanSphere.Core.Features.Roles.Requests;

namespace PlanSphere.Core.Features.Roles.Commands.CreateRole;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    private readonly RoleRequestValidator _requestValidator;
    
    public CreateRoleCommandValidator(RoleRequestValidator requestValidator)
    {
        _requestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
        
        RuleFor(x => x.SourceLevel).NotNull();
        RuleFor(x => x.SourceLevelId).NotNull();
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.Request).NotNull();

        RuleFor(x => x.Request)
            .SetValidator(_requestValidator);
    }
}