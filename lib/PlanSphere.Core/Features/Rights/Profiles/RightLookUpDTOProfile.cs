using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Rights.DTOs;

namespace PlanSphere.Core.Features.Rights.Profiles;

public class RightLookUpDTOProfile : Profile
{
    public RightLookUpDTOProfile()
    {
        CreateMap<Right, RightLookUpDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
    }
}