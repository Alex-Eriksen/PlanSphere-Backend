using AutoMapper;
using PlanSphere.Core.Features.Address.Requests;

namespace PlanSphere.Core.Features.Address.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<AddressRequest, Domain.Entities.Address>()
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