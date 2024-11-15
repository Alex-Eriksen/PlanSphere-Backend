using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Infrastructure.Repositories;

public class TeamRepository(IPlanSphereDatabaseContext context, ILogger<TeamRepository> logger) : ITeamRepository
{
    private readonly IPlanSphereDatabaseContext _context = context ?? throw new ArgumentNullException(nameof(context));
    private readonly ILogger<TeamRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<Team> CreateAsync(Team request, CancellationToken cancellationToken)
    {
        _context.Teams.Add(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<Team> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        var team = await _context.Teams
            .Include(x => x.Address)
            .Include(x => x.Address).ThenInclude(x => x.Parent).ThenInclude(x => x.Parent).ThenInclude(x => x.Parent)
            .Include(x => x.Department).ThenInclude(x => x.Settings)
            .Include(x => x.Settings).ThenInclude(x => x.DefaultWorkSchedule).ThenInclude(x => x.Parent).ThenInclude(x => x.Parent).ThenInclude(x => x.WorkScheduleShifts)
            .Include(x => x.Settings).ThenInclude(x => x.DefaultWorkSchedule).ThenInclude(x => x.Parent).ThenInclude(x => x.WorkScheduleShifts)
            .Include(x => x.Settings).ThenInclude(x => x.DefaultWorkSchedule).ThenInclude(x => x.WorkScheduleShifts)
            .SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (team == null)
        {
            _logger.LogInformation("Could not find the team with id: [{teamId}]. Team doesn't exist", id);
            throw new KeyNotFoundException($"Could not find team with id: [{id}]. Team doesn't exist");
        }

        return team;
    }

    public async Task<Team> UpdateAsync(ulong id, Team request, CancellationToken cancellationToken)
    {
        _context.Teams.Update(request);
        await _context.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<Team> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        var team = await _context.Teams.SingleOrDefaultAsync(t => t.Id == id, cancellationToken);
        if (team == null)
        {
            _logger.LogInformation("Could not find the team with id: [{teamId}]. Team doesn't exist", id);
            throw new KeyNotFoundException($"Could not find team with id: [{id}]. Team doesn't exist");
        }
        _context.Teams.Remove(team);
        await _context.SaveChangesAsync(cancellationToken);
        return team;
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Team> GetQueryable()
    {
        return _context.Teams
            .Include(x => x.Address).ThenInclude(x => x.ZipCode).ThenInclude(x => x.Country)
            .Include(x => x.Address).ThenInclude(x => x.Parent).ThenInclude(x => x.Parent).ThenInclude(x => x.Parent)
            .Include(x => x.Settings)
            .AsNoTracking()
            .AsQueryable();
    }
}