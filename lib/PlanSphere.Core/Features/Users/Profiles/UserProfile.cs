using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Users.Commands.CreateUser;

namespace PlanSphere.Core.Features.Users.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserCommand, User>()
            .ForMember(dest => dest.OrganisationId, opt => opt.MapFrom(src => src.OrganisationId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.UserRequest.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.UserRequest.LastName))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.UserRequest.Birthday))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserRequest.Email))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.UserRequest.Address))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.UserRequest.PhoneNumber))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserId));
    }
}