using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Organisations.Requests;

namespace PlanSphere.Core.Features.Organisations.Profiles;

public class OrganisationProfile : Profile
{
    public OrganisationProfile()
    {
        CreateMap<OrganisationRequest, Organisation>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.LogoUrl))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Settings, opt => opt.MapFrom(src => src.Settings));
        
        CreateMap<Organisation, OrganisationRequest>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.LogoUrl))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Settings, opt => opt.MapFrom(src => src.Settings));
        
        CreateMap<OrganisationUpdateRequest, Organisation>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.LogoUrl))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<OrganisationSettingsRequest, OrganisationSettings>()
            .ForMember(dest => dest.DefaultRoleId, opt => opt.MapFrom(src => src.DefaultRoleId))
            .ForMember(dest => dest.DefaultWorkScheduleId, opt => opt.MapFrom(src => src.DefaultWorkScheduleId));
        
        CreateMap<OrganisationSettings, OrganisationSettingsRequest>()
            .ForMember(dest => dest.DefaultRoleId, opt => opt.MapFrom(src => src.DefaultRoleId))
            .ForMember(dest => dest.DefaultWorkScheduleId, opt => opt.MapFrom(src => src.DefaultWorkScheduleId));
    }
}