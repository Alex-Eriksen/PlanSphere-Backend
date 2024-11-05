using FluentValidation;

namespace PlanSphere.Core.Features.Departments.Queries.GetDepartment;

public class GetDepartmentQueryValidator : AbstractValidator<GetDepartmentQuery>
{
    public GetDepartmentQueryValidator()
    {
        RuleFor(x => x.departmentId)
            .NotNull();
    }
}