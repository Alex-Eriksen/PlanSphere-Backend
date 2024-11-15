using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Users.DTOs;

namespace PlanSphere.Core.Features.Users.Profiles;

public class LoggedInUserDTOProfile : Profile
{
    public LoggedInUserDTOProfile()
    {
        CreateMap<User, LoggedInUserDTO>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrganisationId, opt => opt.MapFrom(src => src.OrganisationId))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.OwnedOrganisations, opt => opt.MapFrom(src => src.OwnedOrganisations.Select(x => x.Id)))
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl));
    }
}