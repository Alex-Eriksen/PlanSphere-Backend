﻿using Domain.Entities;
using PlanSphere.Core.Features.Users.DTOs;

namespace PlanSphere.Core.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetByIdentityIdAsync(string identityId, CancellationToken cancellationToken);
    Task<User> GetByRefreshTokenAsync(string token, CancellationToken cancellationToken);
    Task<bool> IsUserRegisteredAsync(string email, CancellationToken cancellationToken);
    Task RevokeRefreshTokenAsync(string token, string ipAddress, CancellationToken cancellationToken);
    Task<RefreshTokenDTO> RefreshTokenAsync(string token, string ipAddress, CancellationToken cancellationToken);
    Task<RefreshTokenDTO> AuthenticateAsync(User user, string ipAddress, CancellationToken cancellationToken);
    IQueryable<User> GetQueryableWithRights();
}