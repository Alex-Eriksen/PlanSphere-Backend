using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Companies.DTOs;
using PlanSphere.Core.Features.Companies.Request;

namespace PlanSphere.Core.Features.Companies.Profiles;

public class CompanyToCompanyDTO : Profile
{
    public CompanyToCompanyDTO()
    {
        CreateMap<Company, CompanyDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CVR, opt => opt.MapFrom(src => src.VAT))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.CareOf, opt => opt.MapFrom(src => src.CareOf))
            .ForMember(dest => dest.Contact, opt => opt.MapFrom(src => src.Contact));
    }
}