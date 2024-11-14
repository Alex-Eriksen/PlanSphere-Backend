using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Features.WorkTimes.Requests;
using PlanSphere.Core.Utilities.Helpers.Mapper;

namespace PlanSphere.Core.Features.WorkTimes.Profiles;

public class WorkTimeProfile : Profile
{
    public WorkTimeProfile()
    {
        CreateMap<WorkTimeRequest, WorkTime>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom((src, dest, destMember, context) => MappingHelpers.GetIdentifierFromContext(context, MappingKeys.UserId)))
            .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime))
            .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndDateTime))
            .ForMember(dest => dest.WorkTimeType, opt => opt.MapFrom(src => src.WorkTimeType))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.CreatedBy, opt =>
            {
                opt.PreCondition(src => src.LoggedBy is not null);
                opt.MapFrom(src => src.LoggedBy);
            });
    }
}