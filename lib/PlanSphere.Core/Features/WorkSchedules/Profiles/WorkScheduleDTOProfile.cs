using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.WorkSchedules.DTOs;

namespace PlanSphere.Core.Features.WorkSchedules.Profiles;

public class WorkScheduleDTOProfile : Profile
{
    public WorkScheduleDTOProfile()
    {
        CreateMap<WorkSchedule, WorkScheduleDTO>()
            .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent))
            .ForMember(dest => dest.IsDefaultWorkSchedule, opt => opt.MapFrom(src => src.IsDefaultWorkSchedule))
            .ForMember(dest => dest.WorkScheduleShifts, opt => opt.MapFrom(src => src.WorkScheduleShifts));
    }
}