using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.WorkSchedules.DTOs;

namespace PlanSphere.Core.Features.WorkSchedules.Profiles;

public class WorkScheduleLookUpDTOProfile : Profile
{
    public WorkScheduleLookUpDTOProfile()
    {
        CreateMap<WorkSchedule, WorkScheduleLookUpDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));
    }
}