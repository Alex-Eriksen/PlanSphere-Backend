using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.WorkSchedule.Commands.CreateWorkSchedule;
using PlanSphere.Core.Features.WorkSchedule.Request;

namespace PlanSphere.Core.Features.WorkSchedule.Profiles;

public class WorkScheduleProfile : Profile
{
    public WorkScheduleProfile()
    {
        CreateMap<WorkScheduleRequest, Domain.Entities.WorkSchedule>()
            .ForMember(dest => dest.WorkScheduleShifts, opt => opt.MapFrom(src => src.WorkScheduleShifts))
            .ForMember(dest => dest.IsDefaultWorkSchedule, opt => opt.MapFrom(src => src.IsDefaultWorkSchedule));
        
        CreateMap<WorkScheduleShiftRequest, WorkScheduleShift>()
            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
            .ForMember(dest => dest.Day, opt => opt.MapFrom(src => src.Day))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
    }
}