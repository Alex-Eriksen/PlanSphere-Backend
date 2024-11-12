using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Companies.DTOs;

namespace PlanSphere.Core.Features.Companies.Profiles;

public class CompanySettingsDTOProfile : Profile
{
    public CompanySettingsDTOProfile()
    {
        CreateMap<CompanySettings, CompanySettingsDTO>()
            .ForMember(dest => dest.DefaultRole, opt => opt.MapFrom(src => src.DefaultRole))
            .ForMember(dest => dest.DefaultWorkSchedule, opt => opt.MapFrom(src => src.DefaultWorkSchedule))
            .ForMember(dest => dest.InheritDefaultWorkSchedule, opt => opt.MapFrom(src => src.InheritDefaultWorkSchedule));
    }
}