﻿using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Extensions;
using PlanSphere.Core.Features.JobTitles.DTOs;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Interfaces.Services;

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
        _logger.BeginScope("Fetching job titles");
        
        _logger.LogInformation("Fetching job titles on {sourceLevel} with id: [{sourceLevelId}].", request.SourceLevel, request.SourceLevelId);
        var query = GetJobTitles(request);
        _logger.LogInformation("Fetched job titles on {sourceLevel} with id: [{sourceLevelId}].", request.SourceLevel, request.SourceLevelId);
        
        query = SearchQuery(request.Search, query);
        query = SortQuery(request, query);

        var paginatedResponse = await _paginationService.PaginateAsync<JobTitle, JobTitleDTO>(query, request, opt =>
            {
                opt.Items["SourceLevelId"] = request.SourceLevelId;
                opt.Items["SourceLevel"] = request.SourceLevel;
            });


        return paginatedResponse;
    }

    private IQueryable<JobTitle> GetJobTitles(ListJobTitlesQuery request)
    {
        var query = _jobTitleRepository.GetQueryable();
    
        query = request.SourceLevel switch
        {
            SourceLevel.Organisation => query.Where(x => x.OrganisationJobTitle != null && x.OrganisationJobTitle.OrganisationId == request.SourceLevelId), 
            SourceLevel.Company => _jobTitleRepository.GetCompanyJobTitles(request.SourceLevelId, request.OrganisationId, query),
            SourceLevel.Department => _jobTitleRepository.GetDepartmentJobTitles(request.SourceLevelId, query),
            SourceLevel.Team => _jobTitleRepository.GetTeamJobTitles(request.SourceLevelId, query),
            _ => throw new ArgumentOutOfRangeException(nameof(SourceLevel), request.SourceLevel, null)
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
            JobTitleSortBy.Active => SortByIsInheritanceActive(request, query),
            _ => throw new ArgumentOutOfRangeException(nameof(JobTitleSortBy), request.SortBy, null)
        };
    }

    private IQueryable<JobTitle> SortByIsInheritanceActive(ListJobTitlesQuery request, IQueryable<JobTitle> query)
    {
        return request.SourceLevel switch
        {
            SourceLevel.Organisation => query.OrderByExpression(x => x.OrganisationJobTitle.IsInheritanceActive, request.SortDescending ),
            SourceLevel.Company => query.OrderByExpression(x => x.CompanyJobTitle.IsInheritanceActive, request.SortDescending ),
            SourceLevel.Department => query.OrderByExpression(x => x.DepartmentJobTitle.IsInheritanceActive, request.SortDescending ),
            SourceLevel.Team => query.OrderByExpression(x => x.TeamJobTitle.IsInheritanceActive, request.SortDescending ),
            _ => throw new ArgumentOutOfRangeException(nameof(SourceLevel), request.SourceLevel, null)
        };
    }
}