using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Roles.Requests;

namespace PlanSphere.Core.Features.Roles.Profiles;

public class CompanyRoleRightProfile : Profile
{
    public CompanyRoleRightProfile()
    {
        CreateMap<RoleRightRequest, CompanyRoleRight>()
            .ForMember(dest => dest.RightId, opt => opt.MapFrom(src => src.RightId))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.SourceLevelId));
    }
}