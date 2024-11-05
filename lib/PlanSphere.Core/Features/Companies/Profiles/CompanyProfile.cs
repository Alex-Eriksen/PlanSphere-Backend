using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Companies.Request;
using PlanSphere.Core.Features.Companies.Commands.CreateCompany;

namespace PlanSphere.Core.Features.Companies.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompanyRequest, Company>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.VAT, opt => opt.MapFrom(src => src.CVR))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.CareOf, opt => opt.MapFrom(src => src.CareOf))
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactName))
                .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.ContactEmail))
                .ForMember(dest => dest.ContactPhoneNumber, opt => opt.MapFrom(src => src.ContactPhoneNumber));
                

            CreateMap<CompanyUpdateRequest, Company>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.VAT, opt => opt.MapFrom(src => src.CVR))
                .ForMember(dest => dest.LogoUrl, opt => opt.Ignore())
                .ForMember(dest => dest.CareOf, opt => opt.MapFrom(src => src.CareOf))
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactName))
                .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.ContactEmail))
                .ForMember(dest => dest.ContactPhoneNumber, opt => opt.MapFrom(src => src.ContactPhoneNumber))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

            CreateMap<CreateCompanyCommand, Company>()
                .ForMember(dest => dest.OrganisationId, opt => opt.MapFrom(src => src.OrganisationId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Request.Name))
                .ForMember(dest => dest.VAT, opt => opt.MapFrom(src => src.Request.CVR))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Request.Address))
                .ForMember(dest => dest.CareOf, opt => opt.MapFrom(src => src.Request.CareOf))
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.Request.ContactName))
                .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.Request.ContactEmail))
                .ForMember(dest => dest.ContactPhoneNumber, opt => opt.MapFrom(src => src.Request.ContactPhoneNumber));

        }
    }
}