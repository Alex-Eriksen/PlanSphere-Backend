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
            .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime.ToString("o", CultureInfo.InvariantCulture)))
            .ForMember(dest => dest.EndDateTime, opt =>
            {
                opt.PreCondition(src => src.EndDateTime is not null);
                opt.MapFrom(src => src.EndDateTime.Value.ToString("o", CultureInfo.InvariantCulture));
            })
            .ForMember(dest => dest.WorkTimeType, opt => opt.MapFrom(src => src.WorkTimeType))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
    }
}