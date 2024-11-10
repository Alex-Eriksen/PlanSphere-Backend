using AutoMapper;
using PlanSphere.Core.Features.Addresses.Requests;

namespace PlanSphere.Core.Features.Addresses.Profiles;

public class AddressRequestProfile : Profile
{
    public AddressRequestProfile()
    {
        CreateMap<Domain.Entities.Address, AddressRequest>()
            .ForMember(dest => dest.StreetName, opt => opt.MapFrom(src => src.StreetName))
            .ForMember(dest => dest.HouseNumber, opt => opt.MapFrom(src => src.HouseNumber))
            .ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src.Floor))
            .ForMember(dest => dest.Door, opt => opt.MapFrom(src => src.Door))
            .ForMember(dest => dest.PostalCode, opt =>
            {
                opt.PreCondition(src => src.PostalCode is not null);
                opt.MapFrom(src => src.PostalCode);
            })
            .ForMember(dest => dest.CountryId, opt =>
            {
                opt.PreCondition(src => src.CountryId is not null);
                opt.MapFrom(src => src.CountryId);
            });
    }
}