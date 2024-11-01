using MediatR;
using PlanSphere.Core.Abstract;
using PlanSphere.Core.Enums.SortByColumns;
using PlanSphere.Core.Features.Companies.DTOs;
using PlanSphere.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanSphere.Core.Features.Companies.Qurries.ListCompanies;

public record ListCompaniesQuery(string? Search, CompanySortBy SortBy, bool SortDescending) : BasePaginatedQuery, IRequest<IPaginatedResponse<CompanyDTO>>, ISearchableQuery, ISortableQuery<CompanySortBy>
{
    public ulong OrganisationId {  get; set; }
}