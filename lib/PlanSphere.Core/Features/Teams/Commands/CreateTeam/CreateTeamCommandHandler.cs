using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Features.Teams.Commands.CreateTeam;

[HandlerType(HandlerType.SystemApi)]
public class CreateTeamCommandHandler(
    ITeamRepository teamRepository,
    IDepartmentRepository departmentRepository,
    ILogger<CreateTeamCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<CreateTeamCommand>
{
    private readonly ITeamRepository _teamRepository = teamRepository ?? throw new ArgumentNullException(nameof(teamRepository));
    private readonly IDepartmentRepository _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    private readonly ILogger<CreateTeamCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(CreateTeamCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Creating Team");
        _logger.LogInformation("Creating Team on department with id [{departmentId}]", command.DepartmentId);
        var team = _mapper.Map<Team>(command);

        var department = await _departmentRepository.GetByIdAsync(command.DepartmentId, cancellationToken);

        if (command.Request.InheritAddress)
        {
            team.Address.ParentId = department.AddressId;
        }

        team.Settings = new TeamSettings()
        {
            DefaultWorkSchedule = new WorkSchedule
            {
                IsDefaultWorkSchedule = true
            },
            DefaultRole = new Role
            {
                Name = team.Name + "-default-role",
                TeamRoleRights =
                [
                    new TeamRoleRight
                    {
                        RightId = (ulong)Right.View,
                        Team = team
                    }
                ],
                TeamRole = new TeamRole
                {
                    Team = team
                }
            }
        };

        var createdTeam = await _teamRepository.CreateAsync(team, cancellationToken);
        _logger.LogInformation("Created team on department with new id [{teamId}] on department with id [{departmentId}]",command.DepartmentId, createdTeam.Id);
    }
}