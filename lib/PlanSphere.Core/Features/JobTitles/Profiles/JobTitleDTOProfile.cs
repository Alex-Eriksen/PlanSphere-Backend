using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Jobtitles.DTOs;

namespace PlanSphere.Core.Features.Jobtitles.Profiles;

public class JobTitleDTOProfile : Profile
{
    public JobTitleDTOProfile()
    {
        CreateMap<JobTitle, JobTitleDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.IsInheritanceActive, opt => opt.MapFrom<IsInheritanceActiveResolver>());
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
}