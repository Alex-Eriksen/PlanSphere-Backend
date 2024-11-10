using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PlanSphere.Core.Interfaces.Database;

public interface IPlanSphereDatabaseContext
{
    public DbSet<Organisation> Organisations { get; set; }
    public DbSet<OrganisationSettings> OrganisationSettings { get; set; }
    public DbSet<OrganisationRole> OrganisationRoles { get; set; }
    public DbSet<OrganisationJobTitle> OrganisationJobTitles { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanySettings> CompanySettings { get; set; }
    public DbSet<CompanyRole> CompanyRoles { get; set; }
    public DbSet<CompanyBlockedRole> CompanyBlockedRoles { get; set; }
    public DbSet<CompanyJobTitle> CompanyJobTitles { get; set; }
    public DbSet<CompanyBlockedJobTitle> CompanyBlockedJobTitles { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<DepartmentSettings> DepartmentSettings { get; set; }
    public DbSet<DepartmentRole> DepartmentRoles { get; set; }
    public DbSet<DepartmentBlockedRole> DepartmentBlockedRoles { get; set; }
    public DbSet<DepartmentJobTitle> DepartmentJobTitles { get; set; }
    public DbSet<DepartmentBlockedJobTitle> DepartmentBlockedJobTitles { get; set; }
    public DbSet<Team> Teams { get; set; }
    public DbSet<TeamSettings> TeamSettings { get; set; }
    public DbSet<TeamRole> TeamRoles { get; set; }
    public DbSet<TeamBlockedRole> TeamBlockedRoles { get; set; }
    public DbSet<TeamJobTitle> TeamJobTitles { get; set; }
    public DbSet<TeamBlockedJobTitle> TeamBlockedJobTitles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }
    public DbSet<WorkTime> WorkTimes { get; set; }
    public DbSet<WorkTimeLog> WorkTimeLogs { get; set; }
    public DbSet<WorkSchedule> WorkSchedules { get; set; }
    public DbSet<WorkScheduleShift> WorkScheduleShifts { get; set; }
    public DbSet<JobTitle> JobTitles { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<ZipCode> ZipCodes { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Right> Rights { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}