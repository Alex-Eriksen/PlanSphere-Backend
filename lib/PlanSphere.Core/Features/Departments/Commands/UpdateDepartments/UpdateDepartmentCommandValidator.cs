using FluentValidation;

namespace PlanSphere.Core.Features.Departments.Commands.UpdateDepartments;

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotNull();
    }
}