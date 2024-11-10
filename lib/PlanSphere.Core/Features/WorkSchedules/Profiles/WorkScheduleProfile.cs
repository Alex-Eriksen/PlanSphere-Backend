using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.WorkSchedules.Commands.UpdateWorkSchedule;
using PlanSphere.Core.Features.WorkSchedules.Request;

namespace PlanSphere.Core.Features.WorkSchedules.Profiles;

public class WorkScheduleProfile : Profile
{
    public WorkScheduleProfile()
    {
        CreateMap<WorkScheduleRequest, WorkSchedule>()
            .ForMember(dest => dest.WorkScheduleShifts, opt => opt.MapFrom(src => src.WorkScheduleShifts));

        CreateMap<UpdateWorkScheduleCommand, WorkSchedule>()
            .ForMember(dest => dest.WorkScheduleShifts, opt => opt.MapFrom(src => src.Request.WorkScheduleShifts));
    }
}