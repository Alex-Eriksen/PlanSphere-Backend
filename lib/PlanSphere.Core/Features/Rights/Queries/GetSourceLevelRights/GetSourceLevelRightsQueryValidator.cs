using FluentValidation;

namespace PlanSphere.Core.Features.Rights.Queries.GetSourceLevelRights;

public class GetSourceLevelRightsQueryValidator : AbstractValidator<GetSourceLevelRightsQuery>
{
    public GetSourceLevelRightsQueryValidator()
    {
        RuleFor(x => x.UserId).NotNull();
        RuleFor(x => x.SourceLevel).IsInEnum();
        RuleFor(x => x)
            .Must(query =>
            {
                if (query.SourceLevel.HasValue || query.SourceLevelId.HasValue)
                {
                    return query is { SourceLevel: not null, SourceLevelId: not null };
                }

                return true;
            })
            .WithMessage("Both source level and source level id must be filled out at the same time.");
    }
}