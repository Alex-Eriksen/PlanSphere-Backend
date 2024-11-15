using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Roles.DTOs;

namespace PlanSphere.Core.Features.Roles.Profiles;

public class RoleLookUpDTOProfile : Profile
{
    public RoleLookUpDTOProfile()
    {
        CreateMap<Role, RoleLookUpDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));
    }
}