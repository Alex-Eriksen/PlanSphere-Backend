using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Users.DTOs;

namespace PlanSphere.Core.Features.Users.Profiles;

public class UserSettingsDTOProfile : Profile
{
    public UserSettingsDTOProfile()
    {
        CreateMap<UserSettings, UserSettingsDTO>()
            .ForMember(dest => dest.IsBirthdayPrivate, opt => opt.MapFrom(src => src.IsBirthdayPrivate))
            .ForMember(dest => dest.IsEmailPrivate, opt => opt.MapFrom(src => src.IsEmailPrivate))
            .ForMember(dest => dest.IsPhoneNumberPrivate, opt => opt.MapFrom(src => src.IsPhoneNumberPrivate))
            .ForMember(dest => dest.IsAddressPrivate, opt => opt.MapFrom(src => src.IsAddressPrivate))
            .ForMember(dest => dest.WorkSchedule, opt => opt.MapFrom(src => src.WorkSchedule))
            .ForMember(dest => dest.InheritWorkSchedule, opt => opt.MapFrom(src => src.InheritWorkSchedule))
            .ForMember(dest => dest.AutoCheckInOut, opt => opt.MapFrom(src => src.AutoCheckInOut))
            .ForMember(dest => dest.AutoCheckOutDisabled, opt => opt.MapFrom(src => src.AutoCheckOutDisabled));
    }
}