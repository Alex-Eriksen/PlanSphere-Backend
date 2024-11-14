using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.JobTitles.DTOs;

namespace PlanSphere.Core.Features.JobTitles.Profiles;

public class JobTitleLookUpDTOProfile : Profile
{
    public JobTitleLookUpDTOProfile()
    {
        CreateMap<JobTitle, JobTitleLookUpDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));
    }
}