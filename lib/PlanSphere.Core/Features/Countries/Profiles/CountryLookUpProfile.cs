using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Countries.DTOs;

namespace PlanSphere.Core.Features.Countries.Profiles;

public class CountryLookUpProfile : Profile
{
    public CountryLookUpProfile()
    {
        CreateMap<Country, CountryLookUpDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IsoCode))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));
    }
}