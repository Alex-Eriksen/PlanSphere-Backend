using System.Security.Claims;
using Domain.Entities;

namespace PlanSphere.Core.Interfaces;

public interface IJwtHelper
{
    public string GenerateToken(IEnumerable<Claim> claims, DateTime? expiryDate);
    RefreshToken GenerateRefreshToken(string ipAddress);
    public IEnumerable<Claim> GenerateClaims(User user);
}