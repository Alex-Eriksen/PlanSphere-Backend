using FluentValidation;

namespace PlanSphere.Core.Features.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Request.DepartmentName)
            .NotEmpty();
    }
    
}