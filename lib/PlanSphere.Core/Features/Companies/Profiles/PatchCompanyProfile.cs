﻿using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Common.DTOs;
using PlanSphere.Core.Features.Companies.Commands.PatchCompany;
using PlanSphere.Core.Features.Companies.Request;

namespace PlanSphere.Core.Features.Companies.Profiles;

public class PatchCompanyProfile : Profile
{
    public PatchCompanyProfile()
    {
        CreateMap<Company, CompanyUpdateRequest>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CompanyLogo, opt => opt.MapFrom(src => src.LogoUrl))
            .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.Contact.ContactName))
            .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.Contact.ContactEmail))
            .ForMember(dest => dest.ContactPhoneNumber, opt => opt.MapFrom(src => src.Contact.ContactPhoneNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
    }
}