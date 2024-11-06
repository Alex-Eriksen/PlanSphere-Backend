using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Users.Requests;

namespace PlanSphere.Core.Features.Users.Profiles;

public class UserSettingsPatchRequestProfile : Profile
{
    public UserSettingsPatchRequestProfile()
    {
        CreateMap<UserSettings, UserSettingsPatchRequest>()
            .ForMember(dest => dest.IsBirthdayPrivate, opt => opt.MapFrom(src => src.IsBirthdayPrivate))
            .ForMember(dest => dest.IsEmailPrivate, opt => opt.MapFrom(src => src.IsEmailPrivate))
            .ForMember(dest => dest.IsPhoneNumberPrivate, opt => opt.MapFrom(src => src.IsPhoneNumberPrivate))
            .ForMember(dest => dest.IsAddressPrivate, opt => opt.MapFrom(src => src.IsAddressPrivate))
            .ForMember(dest => dest.InheritWorkSchedule, opt => opt.MapFrom(src => src.InheritWorkSchedule))
            .ForMember(dest => dest.AutoCheckInOut, opt => opt.MapFrom(src => src.AutoCheckInOut))
            .ForMember(dest => dest.AutoCheckOutDisabled, opt => opt.MapFrom(src => src.AutoCheckOutDisabled));
    }
}