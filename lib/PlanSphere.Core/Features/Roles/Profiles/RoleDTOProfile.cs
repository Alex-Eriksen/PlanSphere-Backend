using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.DTOs;

namespace PlanSphere.Core.Features.Roles.Profiles;

public class RoleDTOProfile : Profile
{
    public RoleDTOProfile()
    {
        CreateMap<UserRole, RoleDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role.Name))
            .ForMember(dest => dest.Rights, opt => opt.MapFrom(src => 
                src.Role.OrganisationRoleRights
                    .Cast<object>()
                    .Concat(src.Role.CompanyRoleRights)
                    .Concat(src.Role.DepartmentRoleRights)
                    .Concat(src.Role.TeamRoleRights)
            ));
        
        CreateMap<Role, RoleDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Rights, opt => opt.MapFrom(src => 
                src.OrganisationRoleRights
                    .Cast<object>()
                    .Concat(src.CompanyRoleRights)
                    .Concat(src.DepartmentRoleRights)
                    .Concat(src.TeamRoleRights)
            ));
    }
}