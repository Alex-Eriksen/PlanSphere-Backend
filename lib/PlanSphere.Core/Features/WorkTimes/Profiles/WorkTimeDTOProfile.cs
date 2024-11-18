using System.Globalization;
using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.WorkTimes.DTOs;

namespace PlanSphere.Core.Features.WorkTimes.Profiles;

public class WorkTimeDTOProfile : Profile
{
    public WorkTimeDTOProfile()
    {
        CreateMap<WorkTime, WorkTimeDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime))
            .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.StartDateTime, DateTimeKind.Utc)))
            .ForMember(dest => dest.EndDateTime, opt =>
            {
                opt.PreCondition(src => src.EndDateTime is not null);
                opt.MapFrom(src => src.EndDateTime.HasValue 
                    ? DateTime.SpecifyKind(src.EndDateTime.Value, DateTimeKind.Utc)
                    : (DateTime?)null);
            })
            .ForMember(dest => dest.WorkTimeType, opt => opt.MapFrom(src => src.WorkTimeType))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
    }
}