using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Companies.DTOs;


namespace PlanSphere.Core.Features.Companies.Profiles;

public class CompanyDTOProfile : Profile
{
    public CompanyDTOProfile()
    {
        CreateMap<Company, CompanyDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CVR, opt => opt.MapFrom(src => src.VAT))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.CareOf, opt => opt.MapFrom(src => src.CareOf))
            .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactName))
            .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.ContactEmail))
            .ForMember(dest => dest.ContactPhoneNumber, opt => opt.MapFrom(src => src.ContactPhoneNumber));
    }
}