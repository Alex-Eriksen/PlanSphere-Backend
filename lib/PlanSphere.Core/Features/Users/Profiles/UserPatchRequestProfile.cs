using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Users.Requests;

namespace PlanSphere.Core.Features.Users.Profiles;

public class UserPatchRequestProfile : Profile
{
    public UserPatchRequestProfile()
    {
        CreateMap<User, UserPatchRequest>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Settings, opt => opt.MapFrom(src => src.Settings));
    }
}