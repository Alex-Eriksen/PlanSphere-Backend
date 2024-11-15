using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Users.DTOs;

namespace PlanSphere.Core.Features.Users.Profiles;

public class UserLookUpDTOProfile : Profile
{
    public UserLookUpDTOProfile()
    {
        CreateMap<User, UserLookUpDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.FullName));
    }
}