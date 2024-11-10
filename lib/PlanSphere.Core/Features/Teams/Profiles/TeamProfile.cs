using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Teams.Commands.CreateTeam;
using PlanSphere.Core.Features.Teams.Request;

namespace PlanSphere.Core.Features.Teams.Profiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        CreateMap<TeamViewRequest, Team>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Settings, opt => opt.MapFrom(src => src.Settings));


        CreateMap<CreateTeamCommand, Team>()
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Request.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Request.Description))
            .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Request.Identifier))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Request.Address));

        CreateMap<TeamSettingsRequest, TeamSettings>()
            .ForMember(dest => dest.DefaultRoleId, opt => opt.MapFrom(src => src.DefaultRoleId))
            .ForMember(dest => dest.DefaultWorkScheduleId, opt => opt.MapFrom(src => src.DefaultWorkScheduleId));
        CreateMap<TeamSettings, TeamSettingsRequest>()
            .ForMember(dest => dest.DefaultRoleId, opt => opt.MapFrom(src => src.DefaultRoleId))
            .ForMember(dest => dest.DefaultWorkScheduleId, opt => opt.MapFrom(src => src.DefaultWorkScheduleId));
    }
    
}