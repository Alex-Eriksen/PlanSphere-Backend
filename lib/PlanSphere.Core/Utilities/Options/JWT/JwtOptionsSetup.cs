using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace PlanSphere.Core.Utilities.Options.JWT;

public class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    private const string SectionName = "Jwt";
    public void Configure(JwtOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}