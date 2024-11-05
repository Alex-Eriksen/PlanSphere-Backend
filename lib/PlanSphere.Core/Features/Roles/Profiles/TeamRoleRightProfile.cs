using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Roles.Requests;

namespace PlanSphere.Core.Features.Roles.Profiles;

public class TeamRoleRightProfile : Profile
{
    public TeamRoleRightProfile()
    {
        CreateMap<RoleRightRequest, TeamRoleRight>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RightId, opt => opt.MapFrom(src => src.RightId))
            .ForMember(dest => dest.TeamId, opt => opt.MapFrom(src => src.SourceLevelId));
    }
}