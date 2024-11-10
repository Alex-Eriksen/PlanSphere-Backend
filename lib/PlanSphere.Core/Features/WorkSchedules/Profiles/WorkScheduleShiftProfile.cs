using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.WorkSchedules.Request;

namespace PlanSphere.Core.Features.WorkSchedules.Profiles;

public class WorkScheduleShiftProfile : Profile
{
    public WorkScheduleShiftProfile()
    {
        CreateMap<WorkScheduleShiftRequest, WorkScheduleShift>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
            .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
    }
}