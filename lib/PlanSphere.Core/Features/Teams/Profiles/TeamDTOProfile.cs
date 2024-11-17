using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Features.Addresses.DTOs;
using PlanSphere.Core.Features.Teams.DTOs;
using PlanSphere.Core.Utilities.Helpers.Mapper;

namespace PlanSphere.Core.Features.Teams.Profiles;

public class TeamDTOProfile : Profile
{
    public TeamDTOProfile()
    {
        CreateMap<Team, TeamDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
            .ForMember(dest => dest.Address, opt => opt.MapFrom<InheritAddressResolver>())
            .ForMember(dest => dest.InheritAddress, opt => opt.MapFrom(src => src.InheritAddress))
            .ForMember(dest => dest.Settings, opt => opt.MapFrom(src => src.Settings));
    }

    private class InheritAddressResolver : IValueResolver<Team, TeamDTO, AddressDTO>
    {
        public AddressDTO Resolve(Team source, TeamDTO destination, AddressDTO destMember, ResolutionContext context)
        {
            var inheritAddress = MappingHelpers.GetBoolFromContext(context, MappingKeys.InheritAddress);
          
            if (source.InheritAddress && inheritAddress.HasValue && inheritAddress.Value)
            {
                return context.Mapper.Map<AddressDTO>(FindParentAddress(source.Address));
            }

            return context.Mapper.Map<AddressDTO>(source.Address);
        }
        
        private Address FindParentAddress(Address address)
        {
            if (address.Parent != null) return FindParentAddress(address.Parent);
            return address;
        }
    }
}