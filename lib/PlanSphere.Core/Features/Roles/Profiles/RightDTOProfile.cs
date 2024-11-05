using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Roles.DTOs;

namespace PlanSphere.Core.Features.Roles.Profiles;

public class RightDTOProfile : Profile
{
    public RightDTOProfile()
    {
        CreateMap<OrganisationRoleRight, RightDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Right.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Right.Description))
            .ForMember(dest => dest.SourceLevelId, opt => opt.MapFrom(src => src.OrganisationId))
            .ForMember(dest => dest.SourceLevel, opt => opt.MapFrom(src => SourceLevel.Organisation));
        
        CreateMap<CompanyRoleRight, RightDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Right.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Right.Description))
            .ForMember(dest => dest.SourceLevelId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.SourceLevel, opt => opt.MapFrom(src => SourceLevel.Company));
        
        CreateMap<DepartmentRoleRight, RightDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Right.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Right.Description))
            .ForMember(dest => dest.SourceLevelId, opt => opt.MapFrom(src => src.DepartmentId))
            .ForMember(dest => dest.SourceLevel, opt => opt.MapFrom(src => SourceLevel.Department));
        
        CreateMap<TeamRoleRight, RightDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Right.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Right.Description))
            .ForMember(dest => dest.SourceLevelId, opt => opt.MapFrom(src => src.TeamId))
            .ForMember(dest => dest.SourceLevel, opt => opt.MapFrom(src => SourceLevel.Team));
    }
}