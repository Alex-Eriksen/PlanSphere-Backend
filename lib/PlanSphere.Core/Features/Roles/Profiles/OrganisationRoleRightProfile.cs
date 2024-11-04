using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Roles.Requests;

namespace PlanSphere.Core.Features.Roles.Profiles;

public class OrganisationRoleRightProfile : Profile
{
    public OrganisationRoleRightProfile()
    {
        CreateMap<RoleRightRequest, OrganisationRoleRight>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.RightId, opt => opt.MapFrom(src => src.RightId))
            .ForMember(dest => dest.OrganisationId, opt => opt.MapFrom(src => src.SourceLevelId));
    }
}