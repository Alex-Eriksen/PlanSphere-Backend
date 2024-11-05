using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Companies.DTOs;

namespace PlanSphere.Core.Features.Companies.Profiles;

public class CompanyLookUpDTOProfile : Profile
{
    public CompanyLookUpDTOProfile()
    {
        CreateMap<Company, CompanyLookUpDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));
    }
}