using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Teams.DTOs;

namespace PlanSphere.Core.Features.Teams.Profiles;

public class TeamDTOProfile : Profile
{
    public TeamDTOProfile()
    {
        CreateMap<Team, TeamDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.InheritAddress, opt => opt.MapFrom(src => src.InheritAddress))
            .ForMember(dest => dest.Settings, opt => opt.MapFrom(src => src.Settings));
    }
}