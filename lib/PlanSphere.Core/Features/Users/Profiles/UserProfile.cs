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
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Request.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Request.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Request.LastName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Request.Address))
            .ForMember(dest => dest.CreatedBy, opt =>
            {
                opt.PreCondition(src => src.UserId is not 0);
                opt.MapFrom(src => src.UserId);
            });

    }
}