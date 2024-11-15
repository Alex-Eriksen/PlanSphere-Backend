using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Addresses.DTOs;

namespace PlanSphere.Core.Features.Addresses.Profiles;

public class AddressDTOProfile : Profile
{
    public AddressDTOProfile()
    {
        CreateMap<Address, AddressDTO>()
            .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent))
            .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.StreetName))
            .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.HouseNumber))
            .ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src.Floor))
            .ForMember(dest => dest.Door, opt => opt.MapFrom(src => src.Door))
            .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.PostalCode))
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId));
    }
}