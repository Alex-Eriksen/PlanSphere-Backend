using MediatR;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.Jobtitles.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.JobTitles.Queries.ListJobTitles;

public record ListJobTitlesQuery(string? Search, JobTitleSortBy SortBy, bool SortDescending, SourceLevel SourceLevel, ulong SourceLevelId) : BasePaginatedQuery, IRequest<IPaginatedResponse<JobTitleDTO>>, ISearchableQuery, ISortableQuery<JobTitleSortBy>
{
    public ulong OrganisationId { get; set; }
}