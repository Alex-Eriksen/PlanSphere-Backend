using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using PlanSphere.Core.Features.Users.Requests;

namespace PlanSphere.Core.Features.Users.Commands.PatchUser;

public record PatchUserCommand(ulong UserId, JsonPatchDocument<UserPatchRequest> Request) : IRequest;
