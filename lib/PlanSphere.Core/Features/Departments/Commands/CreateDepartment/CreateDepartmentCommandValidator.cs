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
        RuleFor(x => x.Request.Description)
            .MaximumLength(FieldLengthConstants.Long);
        RuleFor(x => x.Request.Building)
            .MaximumLength(FieldLengthConstants.Short);

    }
}