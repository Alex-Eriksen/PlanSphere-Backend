using MediatR;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Users.Requests;

namespace PlanSphere.Core.Features.Users.Commands.CreateUser;

public record CreateUserCommand(ulong OrganisationId, SourceLevel SourceLevel, ulong SourceLevelId, ulong UserId, UserRequest UserRequest) : IRequest;