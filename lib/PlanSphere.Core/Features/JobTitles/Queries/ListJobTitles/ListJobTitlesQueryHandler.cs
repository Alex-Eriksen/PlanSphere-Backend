using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Extensions;
using PlanSphere.Core.Features.Jobtitles.DTOs;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Services.Interfaces;

namespace PlanSphere.Core.Features.JobTitles.Queries.ListJobTitles;

[HandlerType(HandlerType.SystemApi)]
public class ListJobTitlesQueryHandler(
    IJobTitleRepository jobTitleRepository,
    ILogger<ListJobTitlesQueryHandler> logger,
    IPaginationService paginationService
) : IRequestHandler<ListJobTitlesQuery, IPaginatedResponse<JobTitleDTO>>
{
    private readonly IJobTitleRepository _jobTitleRepository = jobTitleRepository ?? throw new ArgumentNullException(nameof(jobTitleRepository));
    private readonly ILogger<ListJobTitlesQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPaginationService _paginationService = paginationService ?? throw new ArgumentNullException(nameof(paginationService));
    
    public async Task<IPaginatedResponse<JobTitleDTO>> Handle(ListJobTitlesQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Fetching job titles on {sourceLevel} with id: [{sourceLevelId}].", request.SourceLevel, request.SourceLevelId);
        var query = GetJobTitles(request);
        _logger.LogInformation("Fetched job titles on {sourceLevel} with id: [{sourceLevelId}].", request.SourceLevel, request.SourceLevelId);

        query = SearchQuery(request.Search, query);
        query = SortQuery(request, query);

        var paginatedResponse = await _paginationService.PaginateAsync<JobTitle, JobTitleDTO>(query, request);

        return paginatedResponse;
    }

    private IQueryable<JobTitle> GetJobTitles(ListJobTitlesQuery request)
    {
        var query = _jobTitleRepository.GetQueryable();
    
        query = request.SourceLevel switch
        {
            SourceLevel.Organisation => query.Where(x => x.OrganisationJobTitle != null && x.OrganisationJobTitle.OrganisationId == request.SourceLevelId), 
            // TODO: Add remaining levels
            _ => query
        };
        
        return query;
    }

    private IQueryable<JobTitle> SearchQuery(string? search, IQueryable<JobTitle> query)
    {
        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(jt => jt.Name.Contains(search));
        }

        return query;
    }

    private IQueryable<JobTitle> SortQuery(ListJobTitlesQuery request, IQueryable<JobTitle> query)
    {
        return request.SortBy switch
        {
            JobTitleSortBy.Name => query.OrderByExpression(jt => jt.Name, request.SortDescending),
            JobTitleSortBy.Active => query.OrderByExpression(jt => jt.OrganisationJobTitle.IsInheritanceActive, request.SortDescending),
            _ => throw new ArgumentOutOfRangeException(nameof(JobTitleSortBy), request.SortBy, null)
        };
    }
}