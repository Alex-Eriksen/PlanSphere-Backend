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
        CreateMap<JobTitleRequest, JobTitle>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<CreateJobTitleCommand, JobTitle>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Request.Name));
    }
}