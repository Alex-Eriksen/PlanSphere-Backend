using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.JobTitles.Commands.CreateJobTitle;
using PlanSphere.Core.Features.JobTitles.Commands.UpdateJobTitle;

namespace PlanSphere.Core.Features.Jobtitles.Profiles;

public class JobTitleProfile : Profile
{
    public JobTitleProfile()
    {
        CreateMap<CreateJobTitleCommand, JobTitle>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Request.Name));
        
        CreateMap<UpdateJobTitleCommand, JobTitle>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Request.Name))
            .ForMember(dest => dest.OrganisationJobTitle.IsInheritanceActive, opt =>
            {
                opt.PreCondition(src => src.SourceLevel == SourceLevel.Organisation);
                opt.MapFrom(src => src.Request.IsInheritanceActive);
            })
            .ForMember(dest => dest.CompanyJobTitle.IsInheritanceActive, opt =>
            {
                opt.PreCondition(src => src.SourceLevel == SourceLevel.Company);
                opt.MapFrom(src => src.Request.IsInheritanceActive);
            })
            .ForMember(dest => dest.DepartmentJobTitle.IsInheritanceActive, opt =>
            {
                opt.PreCondition(src => src.SourceLevel == SourceLevel.Department);
                opt.MapFrom(src => src.Request.IsInheritanceActive);
            })
            .ForMember(dest => dest.TeamJobTitle.IsInheritanceActive, opt =>
            {
                opt.PreCondition(src => src.SourceLevel == SourceLevel.Team);
                opt.MapFrom(src => src.Request.IsInheritanceActive);
            });
    }
}