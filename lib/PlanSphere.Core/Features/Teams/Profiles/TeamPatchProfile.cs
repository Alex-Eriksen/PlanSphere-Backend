using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Teams.Request;

namespace PlanSphere.Core.Features.Teams.Profiles;

public class TeamPatchProfile : Profile
{
    public TeamPatchProfile()
    {
        CreateMap<Team, TeamPatchRequest>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.InheritDefaultWorkSchedule, opt => opt.MapFrom(src => src.Settings.InheritDefaultWorkSchedule));
        
    }
}