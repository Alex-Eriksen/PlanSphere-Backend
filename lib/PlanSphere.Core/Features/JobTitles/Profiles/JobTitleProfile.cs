using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.JobTitles.Commands.CreateJobTitle;
using PlanSphere.Core.Features.JobTitles.Commands.UpdateJobTitle;
using PlanSphere.Core.Features.JobTitles.Requests;

namespace PlanSphere.Core.Features.JobTitles.Profiles;

public class JobTitleProfile : Profile
{
    public JobTitleProfile()
    {
        CreateMap<CreateJobTitleCommand, Domain.Entities.JobTitle>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Request.Name));
    }
    
    private class InheritanceActiveResolver : IValueResolver<UpdateJobTitleCommand, Domain.Entities.JobTitle, bool>
    {
        public bool Resolve(UpdateJobTitleCommand source, Domain.Entities.JobTitle destination, bool destMember, ResolutionContext context)
        {
            return source.SourceLevel switch
            {
                SourceLevel.Organisation when destination.OrganisationJobTitle != null => source.Request.IsInheritanceActive,
                SourceLevel.Company when destination.CompanyJobTitle != null => source.Request.IsInheritanceActive,
                SourceLevel.Department when destination.DepartmentJobTitle != null => source.Request.IsInheritanceActive,
                SourceLevel.Team when destination.TeamJobTitle != null => source.Request.IsInheritanceActive,
                _ => destMember
            };
        }
    }

}