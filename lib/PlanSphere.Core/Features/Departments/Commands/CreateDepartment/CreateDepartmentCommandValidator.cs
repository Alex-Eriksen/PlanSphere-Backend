﻿using FluentValidation;
using PlanSphere.Core.Constants;

namespace PlanSphere.Core.Features.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Request.Name)
            .NotEmpty()
            .MaximumLength(FieldLengthConstants.Short);

    }
}