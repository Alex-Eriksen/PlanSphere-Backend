using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.JobTitles.DTOs;
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
            .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedByUser.FullName))
            .ForMember(dest => dest.SourceLevel, opt => opt.MapFrom(src => src.SourceLevel))
            .ForMember(dest => dest.IsInheritanceActive, opt => opt.MapFrom<IsInheritanceActiveResolver>());
    }
    
    private class IsInheritanceActiveResolver : IValueResolver<Role, RoleListItemDTO, bool>
    {
        public bool Resolve(Role source, RoleListItemDTO destination, bool destMember, ResolutionContext context)
        {
            if (source.OrganisationRole != null)
            {
                return source.OrganisationRole.IsInheritanceActive;
            }
            if (source.CompanyRole != null)
            {
                return source.CompanyRole.IsInheritanceActive;
            }
            if (source.DepartmentRole != null)
            {
                return source.DepartmentRole.IsInheritanceActive;
            }
            if (source.TeamRole != null)
            {
                return source.TeamRole.IsInheritanceActive;
            }
            return false; 
        }
    }
}