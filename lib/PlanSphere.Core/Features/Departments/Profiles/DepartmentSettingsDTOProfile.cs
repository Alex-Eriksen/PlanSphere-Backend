using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Departments.DTOs;

namespace PlanSphere.Core.Features.Departments.Profiles;

public class DepartmentSettingsDTOProfile : Profile
{
    public DepartmentSettingsDTOProfile()
    {
        CreateMap<DepartmentSettings, DepartmentSettingsDTO>()
            .ForMember(dest => dest.DefaultRole, opt => opt.MapFrom(src => src.DefaultRole))
            .ForMember(dest => dest.DefaultWorkSchedule, opt => opt.MapFrom(src => src.DefaultWorkSchedule))
            .ForMember(dest => dest.InheritDefaultWorkSchedule, opt => opt.MapFrom(src => src.InheritDefaultWorkSchedule));
    }
}