using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Addresses.Requests;
using PlanSphere.Core.Features.Users.Requests;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Commands.PatchUser;

[HandlerType(HandlerType.SystemApi)]
public class PatchUserCommandHandler(
    ILogger<PatchUserCommandHandler> logger,
    IUserRepository userRepository,
    IMapper mapper
) : IRequestHandler<PatchUserCommand>
{
    private readonly ILogger<PatchUserCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(PatchUserCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Patch user");
        _logger.LogInformation("Retrieving user with id: [{userId}]", request.UserId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        _logger.LogInformation("Retrieved user with id: [{userId}]", request.UserId);

        var mappedUser = _mapper.Map<UserPatchRequest>(user);
        request.Request.ApplyTo(mappedUser);
        user = _mapper.Map(mappedUser, user);
        
        if (mappedUser.RoleIds != null)
        {
            user.Roles.RemoveAll(x => !mappedUser.RoleIds.Contains(x.RoleId));
            foreach (var roleId in mappedUser.RoleIds)
            {
                var roleAssignment = user.Roles.SingleOrDefault(x => x.RoleId == roleId);
                if (roleAssignment != null) continue;
                
                roleAssignment = new UserRole() { RoleId = roleId, UserId = request.UserId };
                user.Roles.Add(roleAssignment);
            }
        }

        if (mappedUser.JobTitleIds != null)
        {
            user.JobTitles.RemoveAll(x => !mappedUser.JobTitleIds.Contains(x.JobTitleId));
            foreach (var jobTitleId in mappedUser.JobTitleIds)
            {
                var jobTitleAssignment = user.JobTitles.SingleOrDefault(x => x.JobTitleId == jobTitleId);
                if (jobTitleAssignment != null) continue;
                
                jobTitleAssignment = new UserJobTitle { JobTitleId = jobTitleId, UserId = request.UserId };
                user.JobTitles.Add(jobTitleAssignment);
            }
        }

        if (user.Settings.InheritWorkSchedule == false)
        {
            user.Settings.WorkSchedule.Parent = null;
            user.Settings.WorkSchedule.ParentId = null;
        }

        _logger.LogInformation("Updating user.");
        await _userRepository.UpdateAsync(request.UserId, user, cancellationToken);
        _logger.LogInformation("Updated user.");
    }
}