using FluentValidation;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Extensions.ValidationExtensions;
using PlanSphere.Core.Features.Departments.Request;

namespace PlanSphere.Core.Features.Departments.Commands.PatchDepartments;

public class PatchDepartmentCommandValidator : AbstractValidator<PatchDepartmentCommand>
{
    public PatchDepartmentCommandValidator()
    {
        RuleFor(x => x.PatchDocument)
            .NotNull();
        RuleFor(x => x.DepartmentId)
            .NotNull();

        RuleForEach(x => x.PatchDocument.Operations)
            .AllowOperators(PatchOperators.REPLACE)
            .NotNull(nameof(DepartmentUpdateRequest.Name))
            .NotNull(nameof(DepartmentUpdateRequest.Description));
    }
}