using System.Security.Claims;
using Domain.Entities;

namespace PlanSphere.Core.Interfaces;

public interface IJwtHelper
{
    public string GenerateToken(IEnumerable<Claim> claims, DateTime? expiryDate);
    public IEnumerable<Claim> GenerateClaims(User user);
}