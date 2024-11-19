using PlanSphere.Core.Constants;

namespace PlanSphere.Core.Utilities.Builder;

public class EndpointBuilder(string? initialPath = null)
{
    private string _path = initialPath ?? Environment.GetEnvironmentVariable(EnvironmentConstants.ApiUrl)! + "/api";

    public EndpointBuilder AddEndpoint(string endpoint)
    {
        _path = $"{_path}/{endpoint}";

        return this;
    }

    public EndpointBuilder AddQueryParameter(string name, dynamic value)
    {
        _path = _path.Contains('?') ? $"{_path}&{name}={value}" : $"{_path}?{name}={value}";

        return this;
    }

    public string Build() => _path;
}