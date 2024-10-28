using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Roles.Commands.CreateRole;
using PlanSphere.Core.Features.Roles.Requests;

namespace PlanSphere.Core.Features.Roles.Profiles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<CreateRoleCommand, Role>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Request.Name))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.UserId));
    }
}