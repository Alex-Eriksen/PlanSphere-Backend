using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Organisations.DTOs;

namespace PlanSphere.Core.Features.Organisations.Profiles;

public class OrganisationSettingsDTOProfile : Profile
{
    public OrganisationSettingsDTOProfile()
    {
        CreateMap<OrganisationSettings, OrganisationSettingsDTO>()
            .ForMember(dest => dest.DefaultWorkSchedule, opt => opt.MapFrom(src => src.DefaultWorkSchedule))
            .ForMember(dest => dest.DefaultRole, opt => opt.MapFrom(src => src.DefaultRole));
    }
}