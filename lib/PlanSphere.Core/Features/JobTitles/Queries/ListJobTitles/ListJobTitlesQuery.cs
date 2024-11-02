using MediatR;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.JobTitles.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.JobTitles.Queries.ListJobTitles;

public record ListJobTitlesQuery(string? Search, JobTitleSortBy SortBy, bool SortDescending) : BasePaginatedQuery, IRequest<IPaginatedResponse<JobTitleDTO>>, ISearchableQuery, ISortableQuery<JobTitleSortBy>
{
    public SourceLevel SourceLevel { get; set; }
    public ulong SourceLevelId { get; set; }
    public ulong OrganisationId { get; set; }
}