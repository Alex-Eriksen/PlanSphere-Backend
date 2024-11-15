using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using PlanSphere.Core.Features.Users.Requests;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Users.Commands.PatchUser;

public record PatchUserCommand(ulong UserId, JsonPatchDocument<UserPatchRequest> Request) : IRequest;
