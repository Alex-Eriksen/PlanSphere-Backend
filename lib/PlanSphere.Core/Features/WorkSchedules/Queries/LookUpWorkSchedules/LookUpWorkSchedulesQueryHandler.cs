using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.WorkSchedules.DTOs;
using PlanSphere.Core.Interfaces.Repositories;
using RightEnum = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Features.WorkSchedules.Queries.LookUpWorkSchedules;

[HandlerType(HandlerType.SystemApi)]
public class LookUpWorkSchedulesQueryHandler(
    ILogger<LookUpWorkSchedulesQueryHandler> logger,
    IUserRepository userRepository,
    IWorkScheduleRepository workScheduleRepository,
    IMapper mapper
) : IRequestHandler<LookUpWorkSchedulesQuery, List<WorkScheduleLookUpDTO>>
{
    private readonly ILogger<LookUpWorkSchedulesQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IWorkScheduleRepository _workScheduleRepository = workScheduleRepository ?? throw new ArgumentNullException(nameof(workScheduleRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<WorkScheduleLookUpDTO>> Handle(LookUpWorkSchedulesQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Look up work schedules");
        _logger.LogInformation("Retrieving work schedule available for user with id: [{userId}]", request.UserId);
        var query = GetQuery(request.UserId);
        var workScheduleLookUpDtos = await GetAccessibleWorkSchedulesAsync(query, cancellationToken);
        _logger.LogInformation("Retrieved work schedule available for user with id: [{userId}]", request.UserId);

        return workScheduleLookUpDtos;
    }

    private IQueryable<User> GetQuery(ulong userId)
    {
        var query = _userRepository.GetQueryable().Where(x => x.Id == userId);
        query = query.Include(x => x.Roles).ThenInclude(x => x.Role).ThenInclude(x => x.OrganisationRoleRights).ThenInclude(x => x.Right);
        query = query.Include(x => x.Roles).ThenInclude(x => x.Role).ThenInclude(x => x.CompanyRoleRights).ThenInclude(x => x.Right);
        query = query.Include(x => x.Roles).ThenInclude(x => x.Role).ThenInclude(x => x.DepartmentRoleRights).ThenInclude(x => x.Right);
        query = query.Include(x => x.Roles).ThenInclude(x => x.Role).ThenInclude(x => x.TeamRoleRights).ThenInclude(x => x.Right);
        return query.AsSplitQuery();
    }

    private async Task<List<WorkScheduleLookUpDTO>> GetAccessibleWorkSchedulesAsync(IQueryable<User> userQuery, CancellationToken cancellationToken)
    {
        const ulong viewRightId = (ulong)RightEnum.View;

        var userRoles = await userQuery.SelectMany(x => x.Roles).ToListAsync(cancellationToken);
        
        var workScheduleQuery = _workScheduleRepository.GetQueryable().AsSplitQuery();
        workScheduleQuery = workScheduleQuery.Include(x => x.OrganisationSettings).ThenInclude(x => x.Organisation);
        workScheduleQuery = workScheduleQuery.Include(x => x.CompanySettings).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation);
        workScheduleQuery = workScheduleQuery.Include(x => x.DepartmentSettings).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation);
        workScheduleQuery = workScheduleQuery.Include(x => x.TeamSettings).ThenInclude(x => x.Team).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation);

        var accessibleWorkSchedules = new List<WorkScheduleLookUpDTO>();

        accessibleWorkSchedules.AddRange((await workScheduleQuery
                .Where(ws => ws.OrganisationSettings != null)
                .ToListAsync(cancellationToken))
            .Where(ws => userRoles.Any(ur => ur.Role.OrganisationRoleRights.Any(
                orr => orr.OrganisationId == ws.OrganisationSettings.OrganisationId &&
                       orr.RightId <= viewRightId
            )))
            .Select(ws => new WorkScheduleLookUpDTO
            {
                SourceLevel = SourceLevel.Organisation,
                Id = ws.Id,
                Value = ws.OrganisationSettings.Organisation.Name
            })
            .ToList());

        accessibleWorkSchedules.AddRange((await workScheduleQuery
                .Where(ws => ws.CompanySettings != null)
                .ToListAsync(cancellationToken))
            .Where(ws => userRoles.Any(ur =>
                ur.Role.CompanyRoleRights.Any(crr => crr.CompanyId == ws.CompanySettings.CompanyId && crr.RightId <= viewRightId) ||
                ur.Role.OrganisationRoleRights.Any(orr => orr.OrganisationId == ws.CompanySettings.Company.OrganisationId && orr.RightId <= viewRightId)
            ))
            .Select(ws => new WorkScheduleLookUpDTO
            {
                SourceLevel = SourceLevel.Company,
                Id = ws.Id,
                Value = ws.CompanySettings.Company.Name
            })
            .ToList());

        accessibleWorkSchedules.AddRange((await workScheduleQuery
                .Where(ws => ws.DepartmentSettings != null)
                .ToListAsync(cancellationToken))
            .Where(ws => userRoles.Any(ur =>
                ur.Role.DepartmentRoleRights.Any(drr => drr.DepartmentId == ws.DepartmentSettings.DepartmentId && drr.RightId <= viewRightId) ||
                ur.Role.CompanyRoleRights.Any(crr => crr.CompanyId == ws.DepartmentSettings.Department.CompanyId && crr.RightId <= viewRightId) ||
                ur.Role.OrganisationRoleRights.Any(orr => orr.OrganisationId == ws.DepartmentSettings.Department.Company.OrganisationId && orr.RightId <= viewRightId)
            ))
            .Select(ws => new WorkScheduleLookUpDTO
            {
                SourceLevel = SourceLevel.Department,
                Id = ws.Id,
                Value = ws.DepartmentSettings.Department.Name
            })
            .ToList());

        accessibleWorkSchedules.AddRange((await workScheduleQuery
                .Where(ws => ws.TeamSettings != null)
                .ToListAsync(cancellationToken))
            .Where(ws => userRoles.Any(ur =>
                ur.Role.TeamRoleRights.Any(trr => trr.TeamId == ws.TeamSettings.TeamId && trr.RightId <= viewRightId) ||
                ur.Role.DepartmentRoleRights.Any(drr => drr.DepartmentId == ws.TeamSettings.Team.DepartmentId && drr.RightId <= viewRightId) ||
                ur.Role.CompanyRoleRights.Any(crr => crr.CompanyId == ws.TeamSettings.Team.Department.CompanyId && crr.RightId <= viewRightId) ||
                ur.Role.OrganisationRoleRights.Any(orr => orr.OrganisationId == ws.TeamSettings.Team.Department.Company.OrganisationId && orr.RightId <= viewRightId)
            ))
            .Select(ws => new WorkScheduleLookUpDTO
            {
                SourceLevel = SourceLevel.Team,
                Id = ws.Id,
                Value = ws.TeamSettings.Team.Name
            })
            .ToList());

        return accessibleWorkSchedules;
    }
}