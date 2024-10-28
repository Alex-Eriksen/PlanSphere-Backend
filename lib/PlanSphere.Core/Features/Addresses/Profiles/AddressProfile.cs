using Domain.Entities;
using AutoMapper;
using PlanSphere.Core.Features.Addresses.Requests;

namespace PlanSphere.Core.Features.Addresses.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<AddressRequest, Domain.Entities.Address>()
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId))
            .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.HouseNumber))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.StreetName))
            .ForMember(dest => dest.Door, opt => opt.MapFrom(src => src.Door))
            .ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src.Floor));
    }
}