using System.Security.Claims;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.JsonWebTokens;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Features.Users.DTOs;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Database;
using PlanSphere.Core.Interfaces.Repositories;
using BC = BCrypt.Net.BCrypt;

namespace PlanSphere.Infrastructure.Repositories;

public class UserRepository(IPlanSphereDatabaseContext dbContext, ILogger<UserRepository> logger, IJwtHelper jwtHelper) : IUserRepository
{
    private readonly IPlanSphereDatabaseContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    private readonly ILogger<UserRepository> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IJwtHelper _jwtHelper = jwtHelper ?? throw new ArgumentNullException(nameof(jwtHelper));

    public async Task<User> CreateAsync(User request, CancellationToken cancellationToken)
    {
        _dbContext.Users.Add(request);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return request;
    }

    public async Task<User> GetByIdAsync(ulong id, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(x => x.Settings)
            .Include(x => x.Address)
            .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.OrganisationRoleRights)
                        .ThenInclude(x => x.Right)
            .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.CompanyRoleRights)
                        .ThenInclude(x => x.Right)
            .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.DepartmentRoleRights)
                        .ThenInclude(x => x.Right)
            .Include(x => x.Roles)
                .ThenInclude(x => x.Role)
                    .ThenInclude(x => x.TeamRoleRights)
                        .ThenInclude(x => x.Right)
            .AsSplitQuery()
            .AsNoTracking()
            .SingleOrDefaultAsync(user => user.Id == id, cancellationToken);
        
        if (user == null)
        {
            _logger.LogInformation("Could not find user with id: [{userId}]. User doesn't exist!", id);
            throw new KeyNotFoundException($"Could not find user with id: [{id}]. User doesn't exist!");
        }

        return user;
    }

    public Task<User> UpdateAsync(ulong id, User request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteAsync(ulong id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public IQueryable<User> GetQueryable()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(x => x.Settings)
            .Include(x => x.Address)
            .Include(x => x.RefreshTokens)
            .SingleOrDefaultAsync(user => user.IdentityUserId == identityId, cancellationToken);
        
        if (user == null)
        {
            _logger.LogInformation("Could not find user with identity id: [{identityId}]. User doesn't exist!", identityId);
            throw new KeyNotFoundException($"Could not find user with identity id: [{identityId}]. User doesn't exist!");
        }

        return user;
    }

    public async Task<User> GetByRefreshTokenAsync(string token, CancellationToken cancellationToken)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .Include(x => x.User)
                .ThenInclude(x => x.Roles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.OrganisationRoleRights)
                            .ThenInclude(x => x.Right)
            .Include(x => x.User)
                .ThenInclude(x => x.Roles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.CompanyRoleRights)
                            .ThenInclude(x => x.Right)
            .Include(x => x.User)
                .ThenInclude(x => x.Roles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.DepartmentRoleRights)
                            .ThenInclude(x => x.Right)
            .Include(x => x.User)
                .ThenInclude(x => x.Roles)
                    .ThenInclude(x => x.Role)
                        .ThenInclude(x => x.TeamRoleRights)
                            .ThenInclude(x => x.Right)
            .AsSplitQuery()
            .SingleOrDefaultAsync(t => t.Token == token, cancellationToken);
        
        if (refreshToken is null or { IsActive: false })
        {
            _logger.LogInformation("Invalid refresh token!");
            throw new KeyNotFoundException("Invalid refresh token!");
        }

        return refreshToken.User;
    }

    public async Task<bool> IsUserRegisteredAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email == email, cancellationToken);
    }

    public async Task RevokeRefreshTokenAsync(string token, string ipAddress, CancellationToken cancellationToken)
    {
        var refreshToken = await _dbContext.RefreshTokens.SingleOrDefaultAsync(t => t.Token == token, cancellationToken);

        if (refreshToken is null or { IsActive: false })
        {
            _logger.LogInformation("Invalid refresh token!");
            throw new KeyNotFoundException("Invalid refresh token!");
        }
        
        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;

        _dbContext.RefreshTokens.Update(refreshToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<RefreshTokenDTO> RefreshTokenAsync(string token, string ipAddress, CancellationToken cancellationToken)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .Include(x => x.User)
            .SingleOrDefaultAsync(t => t.Token == token, cancellationToken);
        
        if (refreshToken is null or { IsActive: false })
        {
            _logger.LogInformation("Invalid refresh token!");
            throw new KeyNotFoundException("Invalid refresh token!");
        }

        var newRefreshToken = _jwtHelper.GenerateRefreshToken(ipAddress);

        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReplacedByToken = newRefreshToken.Token;

        var user = refreshToken.User;
        user.RefreshTokens.Add(newRefreshToken);

        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimsConstants.UserId, user.Id.ToString()),
            new(ClaimsConstants.OrganisationId, user.OrganisationId.ToString()),
            new(ClaimsConstants.Email, user.Email),
            new(ClaimsConstants.FirstName, user.FirstName),
            new(ClaimsConstants.LastName, user.LastName)
        };

        var accessToken = _jwtHelper.GenerateToken(claims, DateTime.UtcNow.AddMinutes(15));

        return new RefreshTokenDTO { RefreshToken = newRefreshToken.Token, AccessToken = accessToken };
    }

    public async Task<RefreshTokenDTO> AuthenticateAsync(User user, string ipAddress, CancellationToken cancellationToken)
    {
        var refreshToken = user.RefreshTokens.SingleOrDefault(t => t.IsActive);
        
        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimsConstants.UserId, user.Id.ToString()),
            new(ClaimsConstants.OrganisationId, user.OrganisationId.ToString()),
            new(ClaimsConstants.Email, user.Email),
            new(ClaimsConstants.FirstName, user.FirstName),
            new(ClaimsConstants.LastName, user.LastName)
        };

        var accessToken = _jwtHelper.GenerateToken(claims, DateTime.UtcNow.AddMinutes(15));
        
        if (refreshToken is not null)
        {
            return new RefreshTokenDTO { RefreshToken = refreshToken.Token, AccessToken = accessToken };
        }
        
        var newRefreshToken = _jwtHelper.GenerateRefreshToken(ipAddress);

        user.RefreshTokens.Add(newRefreshToken);
        
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new RefreshTokenDTO { RefreshToken = newRefreshToken.Token, AccessToken = accessToken };
    }

}