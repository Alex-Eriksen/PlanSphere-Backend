﻿using System.Data;
using FluentValidation;

namespace PlanSphere.Core.Features.WorkSchedule.Commands.CreateWorkSchedule;

public class CreateWorkScheduleCommandValidator : AbstractValidator<CreateWorkScheduleCommand>
{
    public CreateWorkScheduleCommandValidator()
    {
        RuleFor(x => x.Request.SourceLevelId)
            .NotNull();
    }
}