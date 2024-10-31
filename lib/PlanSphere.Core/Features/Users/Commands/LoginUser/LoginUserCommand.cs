using System.Text.Json.Serialization;
using MediatR;
using PlanSphere.Core.Features.Users.DTOs;

namespace PlanSphere.Core.Features.Users.Commands.LoginUser;

public record LoginUserCommand(string Email, string Password) : IRequest<RefreshTokenDTO>
{
    [JsonIgnore]
    public string? IpAddress { get; set; }
}