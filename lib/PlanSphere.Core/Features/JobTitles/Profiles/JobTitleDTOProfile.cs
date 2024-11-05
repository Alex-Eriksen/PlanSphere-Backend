using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.JobTitles.DTOs;

namespace PlanSphere.Core.Features.JobTitles.Profiles;

public class JobTitleDTOProfile : Profile
{
    public JobTitleDTOProfile()
    {
        CreateMap<JobTitle, JobTitleDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.IsInheritanceActive, opt => opt.MapFrom<IsInheritanceActiveResolver>())
            .ForMember(dest => dest.SourceLevel, opt => opt.MapFrom<SourceLevelResolver>());
    }
    
    private class IsInheritanceActiveResolver : IValueResolver<JobTitle, JobTitleDTO, bool>
    {
        public bool Resolve(JobTitle source, JobTitleDTO destination, bool destMember, ResolutionContext context)
        {
            if (source.OrganisationJobTitle != null)
            {
                return source.OrganisationJobTitle.IsInheritanceActive;
            }
            if (source.CompanyJobTitle != null)
            {
                return source.CompanyJobTitle.IsInheritanceActive;
            }
            if (source.DepartmentJobTitle != null)
            {
                return source.DepartmentJobTitle.IsInheritanceActive;
            }
            if (source.TeamJobTitle != null)
            {
                return source.TeamJobTitle.IsInheritanceActive;
            }
            return false; 
        }
    }
    
    private class SourceLevelResolver : IValueResolver<JobTitle, JobTitleDTO, SourceLevel>
    {
        public SourceLevel Resolve(JobTitle source, JobTitleDTO destination, SourceLevel destMember, ResolutionContext context)
        {
            if (source.OrganisationJobTitle != null)
            {
                return SourceLevel.Organisation;
            }
            if (source.CompanyJobTitle != null)
            {
                return SourceLevel.Company;
            }
            if (source.DepartmentJobTitle != null)
            {
                return SourceLevel.Department;
            }
            return SourceLevel.Team;
            
            
        }
    }
}