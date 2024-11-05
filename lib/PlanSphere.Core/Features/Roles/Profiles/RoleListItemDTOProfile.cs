using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Constants;
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
            .ForMember(dest => dest.IsInheritanceActive, opt => opt.MapFrom<IsInheritanceActiveResolver>())
            .ForMember(dest => dest.IsDefaultRole, opt => opt.MapFrom<IsDefaultRoleResolver>());
    }
    
    private class IsInheritanceActiveResolver : IValueResolver<Role, RoleListItemDTO, bool>
    {
        public bool Resolve(Role source, RoleListItemDTO destination, bool destMember, ResolutionContext context)
        {
            context.Items.TryGetValue(MappingKeys.SourceLevel, out var obj);
            context.Items.TryGetValue(MappingKeys.SourceLevelId, out var obj1);
            Enum.TryParse<SourceLevel>(obj!.ToString(), out var requestSourceLevel);
            ulong.TryParse(obj1.ToString(), out var requestSourceLevelId);

            if (requestSourceLevel != source.SourceLevel)
            {
                return requestSourceLevel switch
                {
                    SourceLevel.Company => !source.BlockedCompanies.Any(x => x.CompanyId == requestSourceLevelId && x.RoleId == source.Id),
                    SourceLevel.Department => !source.BlockedDepartments.Any(x => x.DepartmentId == requestSourceLevelId && x.RoleId == source.Id),
                    SourceLevel.Team => true,
                    SourceLevel.Organisation => throw new ArgumentOutOfRangeException(),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            
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
    
    private class IsDefaultRoleResolver : IValueResolver<Role, RoleListItemDTO, bool>
    {
        public bool Resolve(Role source, RoleListItemDTO destination, bool destMember, ResolutionContext context)
        {
            return source.SourceLevel switch
            {
                SourceLevel.Organisation => source.Id == source.OrganisationRole.Organisation.Settings.DefaultRoleId,
                SourceLevel.Company => source.Id == source.CompanyRole.Company.Settings.DefaultRoleId,
                SourceLevel.Department => source.Id == source.DepartmentRole.Department.Settings.DefaultRoleId,
                SourceLevel.Team => source.Id == source.TeamRole.Team.Settings.DefaultRoleId,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}