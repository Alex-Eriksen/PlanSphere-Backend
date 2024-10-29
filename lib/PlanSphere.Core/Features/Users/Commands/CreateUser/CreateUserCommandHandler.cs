using AutoMapper;
using Domain.Entities;
using MediatR;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Users.Commands.CreateUser;

[HandlerType(HandlerType.SystemApi)]
public class CreateUserCommandHandler(
    IUserRepository userRepository,
    IMapper mapper
) : IRequestHandler<CreateUserCommand>
{
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    
    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var userRequest = _mapper.Map<User>(command);

        await _userRepository.CreateAsync(userRequest, cancellationToken);
        
        switch (command.SourceLevel)
        {
            case SourceLevel.Organisation:
                break;
            case SourceLevel.Company:
                break;
            case SourceLevel.Department:
                break;
            case SourceLevel.Team:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}