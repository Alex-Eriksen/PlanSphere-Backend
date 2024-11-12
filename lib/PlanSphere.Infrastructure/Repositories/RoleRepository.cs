﻿using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;
using Right = Domain.Entities.Right;

namespace PlanSphere.Infrastructure.Repositories;

public class RoleRepository(
    IPlanSphereDatabaseContext dbContext,
    ILogger<RoleRepository> logger
) : IRoleRepository
{
    private readonly IPlanSphereDatabaseContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly ILogger<RoleRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<Role> CreateAsync(Role request, CancellationToken cancellationToken)
    {
        _dbContext.Roles.Add(request);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return request;
    }

    public async Task<Role> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles
            .Include(x => x.OrganisationRole).ThenInclude(x => x.Organisation)
            .Include(x => x.CompanyRole).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.DepartmentRole).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.TeamRole).ThenInclude(x => x.Team).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.CreatedByUser)
            .Include(x => x.OrganisationRoleRights).ThenInclude(x => x.Right)
            .Include(x => x.CompanyRoleRights).ThenInclude(x => x.Right)
            .Include(x => x.DepartmentRoleRights).ThenInclude(x => x.Right)
            .Include(x => x.TeamRoleRights).ThenInclude(x => x.Right)
            .Include(x => x.BlockedCompanies)
            .Include(x => x.BlockedDepartments)
            .AsSplitQuery()
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        
        if (role == null)
        {
            _logger.LogInformation("Couldn't find role with id: [{roleId}]", id);
            throw new KeyNotFoundException($"Couldn't find role with id: [{id}]");
        }

        return role;
    }

    public async Task<Role> UpdateAsync(ulong id, Role request, CancellationToken cancellationToken)
    {
        _dbContext.Roles.Update(request);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return request;
    }

    public async Task<Role> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        var role = await _dbContext.Roles.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (role == null)
        {
            _logger.LogInformation("Couldn't find role with id: [{roleId}]", id);
            throw new KeyNotFoundException($"Couldn't find role with id: [{id}]");
        }

        dbContext.Roles.Remove(role);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return role;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Role> GetQueryable()
    {
        return _dbContext.Roles
            .Include(x => x.OrganisationRole).ThenInclude(x => x.Organisation).ThenInclude(x => x.Settings)
            .Include(x => x.CompanyRole).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.DepartmentRole).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.TeamRole).ThenInclude(x => x.Team).ThenInclude(x => x.Department).ThenInclude(x => x.Company).ThenInclude(x => x.Organisation)
            .Include(x => x.CompanyRole).ThenInclude(x => x.Company).ThenInclude(x => x.Settings)
            .Include(x => x.DepartmentRole).ThenInclude(x => x.Department).ThenInclude(x => x.Settings)
            .Include(x => x.TeamRole).ThenInclude(x => x.Team).ThenInclude(x => x.Settings)
            .Include(x => x.UpdatedByUser)
            .Include(x => x.CreatedByUser)
            .Include(x => x.OrganisationRoleRights).ThenInclude(x => x.Right)
            .Include(x => x.CompanyRoleRights).ThenInclude(x => x.Right)
            .Include(x => x.DepartmentRoleRights).ThenInclude(x => x.Right)
            .Include(x => x.TeamRoleRights).ThenInclude(x => x.Right)
            .Include(x => x.BlockedCompanies)
            .Include(x => x.BlockedDepartments)
            .AsNoTracking()
            .AsQueryable();
    }
    
    public async Task<List<Right>> GetRightsAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Rights.ToListAsync(cancellationToken);
    }

    public async Task<Role> ToggleRoleInheritanceAsync(Role role, CancellationToken cancellationToken)
    {
        switch (role.SourceLevel)
        {
            case SourceLevel.Organisation:
                role.OrganisationRole!.IsInheritanceActive = !role.OrganisationRole.IsInheritanceActive;
                break;
            case SourceLevel.Company:
                role.CompanyRole!.IsInheritanceActive = !role.CompanyRole.IsInheritanceActive;
                break;
            case SourceLevel.Department:
                role.DepartmentRole!.IsInheritanceActive = !role.DepartmentRole.IsInheritanceActive;
                break;
            case SourceLevel.Team:
                role.TeamRole!.IsInheritanceActive = !role.TeamRole.IsInheritanceActive;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        await UpdateAsync(role.Id, role, cancellationToken);
        return role;
    }

    public IQueryable<Role> GetCompanyRoles(ulong companyId, ulong organisationId, IQueryable<Role> query)
    {
        return query.Where(x => (x.CompanyRole != null && x.CompanyRole.CompanyId == companyId) ||
                                (x.OrganisationRole != null && x.OrganisationRole.IsInheritanceActive && x.OrganisationRole.OrganisationId == organisationId));
    }

    public IQueryable<Role> GetDepartmentRoles(ulong departmentId, IQueryable<Role> query)
    {
        var department = _dbContext.Departments
            .Where(t => t.Id == departmentId)
            .Select(t => new { t.CompanyId, t.Company.OrganisationId })
            .FirstOrDefault();
        
        return query.Where(x =>
            (x.DepartmentRole != null && x.DepartmentRole.DepartmentId == departmentId) ||
            
            (x.OrganisationRole != null &&
             x.OrganisationRole.IsInheritanceActive &&
             x.OrganisationRole.OrganisationId == department.OrganisationId &&
             x.BlockedCompanies.All(cbr => cbr.CompanyId != department.CompanyId && cbr.RoleId != x.OrganisationRole.RoleId)) ||
            
            (x.CompanyRole != null &&
             x.CompanyRole.IsInheritanceActive &&
             x.BlockedCompanies.All(cbr => cbr.CompanyId != department.CompanyId && cbr.RoleId != x.CompanyRole.RoleId)));
    }

    public IQueryable<Role> GetTeamRoles(ulong teamId, IQueryable<Role> query)
    {
        var team = _dbContext.Teams
            .Where(t => t.Id == teamId)
            .Select(t => new { t.DepartmentId, t.Department.CompanyId, t.Department.Company.OrganisationId })
            .FirstOrDefault();

        return query.Where(x =>
            (x.TeamRole != null && x.TeamRole.TeamId == teamId) ||

            (x.OrganisationRole != null &&
             x.OrganisationRole.IsInheritanceActive &&
             x.OrganisationRole.OrganisationId == team.OrganisationId &&
             x.BlockedCompanies.All(cbr =>
                 cbr.CompanyId != team.CompanyId && cbr.RoleId != x.OrganisationRole.RoleId) &&
             x.BlockedDepartments.All(dbr =>
                 dbr.DepartmentId != team.DepartmentId && dbr.RoleId != x.OrganisationRole.RoleId)) ||

            (x.CompanyRole != null &&
             x.CompanyRole.IsInheritanceActive &&
             x.BlockedCompanies.All(cbr => cbr.CompanyId != team.CompanyId && cbr.RoleId != x.CompanyRole.RoleId) &&
             x.BlockedDepartments.All(dbr => dbr.DepartmentId != team.DepartmentId && dbr.RoleId != x.CompanyRole.RoleId)) ||

            (x.DepartmentRole != null &&
             x.DepartmentRole.IsInheritanceActive &&
             x.BlockedCompanies.All(cbr => cbr.CompanyId != team.CompanyId && cbr.RoleId != x.DepartmentRole.RoleId) &&
             x.BlockedDepartments.All(dbr => dbr.DepartmentId != team.DepartmentId && dbr.RoleId != x.DepartmentRole.RoleId)));

    }
}