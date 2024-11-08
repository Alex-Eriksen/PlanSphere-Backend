using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.WorkSchedules.DTOs;

namespace PlanSphere.Core.Features.WorkSchedules.Profiles;

public class WorkScheduleShiftDTOProfile : Profile
{
    public WorkScheduleShiftDTOProfile()
    {
        CreateMap<WorkScheduleShift, WorkScheduleShiftDTO>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
            .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
    }
}