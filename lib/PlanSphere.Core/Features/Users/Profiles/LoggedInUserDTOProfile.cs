using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Users.DTOs;

namespace PlanSphere.Core.Features.Users.Profiles;

public class LoggedInUserDTOProfile : Profile
{
    public LoggedInUserDTOProfile()
    {
        CreateMap<User, LoggedInUserDTO>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles));
    }
}