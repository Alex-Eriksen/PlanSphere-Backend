using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Teams.DTOs;

namespace PlanSphere.Core.Features.Teams.Profiles;

public class TeamLookUpDTOProfile : Profile
{
    public TeamLookUpDTOProfile()
    {
        CreateMap<Team, TeamLookUpDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));
    }
}