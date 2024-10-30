using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace PlanSphere.Core.Utilities.Options.JWT;

public class JwtBearerOptionsSetup(IOptions<JwtOptions> options) : IConfigureOptions<JwtBearerOptions>
{
    private readonly IOptions<JwtOptions> _options = options ?? throw new ArgumentNullException(nameof(options));

    public void Configure(JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = _options.Value.Issuer,
            ValidAudience = _options.Value.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_options.Value.SecretKey))
        };
    }
}