using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.ZipCodes.DTOs;

namespace PlanSphere.Core.Features.ZipCodes.Profiles;

public class ZipCodeLookUpProfile : Profile
{
    public ZipCodeLookUpProfile()
    {
        CreateMap<ZipCode, ZipCodeLookUpDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));
    }
    
}