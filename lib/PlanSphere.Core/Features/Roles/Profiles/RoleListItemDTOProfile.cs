using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Roles.DTOs;

namespace PlanSphere.Core.Features.Roles.Profiles;

public class RoleListItemDTOProfile : Profile
{
    public RoleListItemDTOProfile()
    {
        CreateMap<Role, RoleListItemDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Rights, opt => opt.MapFrom(src => src.OrganisationRoleRights.Count + src.CompanyRoleRights.Count + src.DepartmentRoleRights.Count + src.TeamRoleRights.Count))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByUser.FullName));
    }
}