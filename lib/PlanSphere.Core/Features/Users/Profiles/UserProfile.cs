using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Users.Commands.CreateUser;
using PlanSphere.Core.Features.Users.Commands.UpdateUser;
using PlanSphere.Core.Features.Users.DTOs;
using PlanSphere.Core.Features.Users.Queries.ListUsers;
using PlanSphere.Core.Features.Users.Requests;
using PlanSphere.Core.Features.Users.Requests;

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
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Request.PhoneNumber))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Request.Address))
            .ForMember(dest => dest.CreatedBy, opt =>
            {
                opt.PreCondition(src => src.UserId is not 0);
                opt.MapFrom(src => src.UserId);
            });

        CreateMap<User, UserListDTO>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl))
            .ForMember(dest => dest.CreatedBy, opt =>
            {
                opt.PreCondition(src => src.Id is not 0);
                opt.MapFrom(src => src.CreatedByUser.FullName);
            })
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

        CreateMap<UserRequest, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));
        
        CreateMap<UserPatchRequest, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Settings, opt => opt.MapFrom(src => src.Settings));
        
        CreateMap<User, UserPatchRequest>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Settings, opt => opt.MapFrom(src => src.Settings));
    }
}