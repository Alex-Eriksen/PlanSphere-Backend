using FluentValidation;
using PlanSphere.Core.Constants;

namespace PlanSphere.Core.Features.Roles.Requests;

public class RoleRequestValidator : AbstractValidator<RoleRequest>
{
    private readonly RoleRightRequestValidator _requestValidator;
    public RoleRequestValidator(RoleRightRequestValidator requestValidator)
    {
        _requestValidator = requestValidator ?? throw new ArgumentNullException(nameof(requestValidator));
        RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(FieldLengthConstants.Short);
        
        RuleForEach(x => x.RoleRightRequests)
            .SetValidator(_requestValidator);
    }
}