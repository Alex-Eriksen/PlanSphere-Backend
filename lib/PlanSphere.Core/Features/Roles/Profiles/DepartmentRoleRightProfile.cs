using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Roles.Requests;

namespace PlanSphere.Core.Features.Roles.Profiles;

public class DepartmentRoleRightProfile : Profile
{
    public DepartmentRoleRightProfile()
    {
        CreateMap<RoleRightRequest, DepartmentRoleRight>()
            .ForMember(dest => dest.RightId, opt => opt.MapFrom(src => src.RightId))
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.SourceLevelId));
    }
}