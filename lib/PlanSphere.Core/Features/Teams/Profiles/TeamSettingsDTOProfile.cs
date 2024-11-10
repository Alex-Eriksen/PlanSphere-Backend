using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Teams.DTOs;

namespace PlanSphere.Core.Features.Teams.Profiles;

public class TeamSettingsDTOProfile : Profile
{
    public TeamSettingsDTOProfile()
    {
        CreateMap<TeamSettings, TeamSettingsDTO>()
            .ForMember(dest => dest.DefaultRole, opt => opt.MapFrom(src => src.DefaultRole))
            .ForMember(dest => dest.DefaultWorkSchedule, opt => opt.MapFrom(src => src.DefaultWorkSchedule));
    }
}