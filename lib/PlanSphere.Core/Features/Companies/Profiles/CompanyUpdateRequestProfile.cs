using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Companies.Request;

namespace PlanSphere.Core.Features.Companies.Profiles;

public class CompanyUpdateRequestProfile : Profile
{
    public CompanyUpdateRequestProfile()
    {
        CreateMap<Company, CompanyUpdateRequest>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CVR, opt => opt.MapFrom(src => src.VAT))
            .ForMember(dest => dest.CareOf, opt => opt.MapFrom(src => src.CareOf))
            .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactName))
            .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.ContactEmail))
            .ForMember(dest => dest.ContactPhoneNumber, opt => opt.MapFrom(src => src.ContactPhoneNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForPath(dest => dest.InheritDefaultWorkSchedule, opt => opt.MapFrom(src => src.Settings.InheritDefaultWorkSchedule));
    }
}