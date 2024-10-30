using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Utilities.Options.JWT;

namespace PlanSphere.Core.Utilities.Helpers.JWT;

public class JwtHelper(IOptions<JwtOptions> options) : IJwtHelper
{
    private readonly IOptions<JwtOptions> _options = options ?? throw new ArgumentNullException(nameof(options));

    public string GenerateToken(IEnumerable<Claim> claims, DateTime? expiryDate)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.Value.SecretKey)),
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            _options.Value.Issuer,
            _options.Value.Audience,
            claims,
            null,
            expiryDate ?? DateTime.UtcNow.AddDays(1),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }

    public IEnumerable<Claim> GenerateClaims(User user)
    {
        return new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(ClaimsConstants.UserId, user.Id.ToString()),
            new(ClaimsConstants.OrganizationId, user.OrganisationId.ToString()),
            new(ClaimsConstants.Email, user.Email),
            new(ClaimsConstants.FirstName, user.FirstName),
            new(ClaimsConstants.LastName, user.LastName)
        };
    }
}