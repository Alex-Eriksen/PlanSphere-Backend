using AutoMapper;
using Domain.Entities;
using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Constants;
using PlanSphere.Core.Features.Roles.Requests;
using PlanSphere.Core.Features.WorkTimes.Commands.CreateWorkTime;
using PlanSphere.Core.Features.WorkTimes.Commands.UpdateWorkTime;
using PlanSphere.Core.Features.WorkTimes.Requests;
using PlanSphere.Core.Utilities.Helpers.Mapper;

namespace PlanSphere.Core.Features.WorkTimes.Profiles;

public class WorkTimeLogProfile : Profile
{
    public WorkTimeLogProfile()
    {
        CreateMap<WorkTimeRequest, WorkTimeLog>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom((src, dest, destMember, context) => MappingHelpers.GetIdentifierFromContext(context, MappingKeys.UserId)))
            .ForMember(dest => dest.ActionType, opt => opt.MapFrom((src, dest, destMember, context) => MappingHelpers.GetEnumFromContext<ActionType>(context, MappingKeys.ActionType) ?? ActionType.Create))
            .ForMember(dest => dest.OldStartDateTime, opt => opt.MapFrom((src, dest, destMember, context) => 
            {
                var workTime = MappingHelpers.GetFromContext<WorkTime>(context, MappingKeys.WorkTime);
                return workTime?.StartDateTime;
            }))
            .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime))
            .ForMember(dest => dest.OldEndDateTime, opt => opt.MapFrom((src, dest, destMember, context) =>
            {
                var workTime = MappingHelpers.GetFromContext<WorkTime>(context, MappingKeys.WorkTime);
                return workTime?.EndDateTime;
            }))
            .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndDateTime))
            .ForMember(dest => dest.OldWorkTimeType, opt => opt.MapFrom((src, dest, destMember, context) =>
            {
                var workTime = MappingHelpers.GetFromContext<WorkTime>(context, MappingKeys.WorkTime);
                return workTime?.WorkTimeType;
            }))
            .ForMember(dest => dest.WorkTimeType, opt => opt.MapFrom(src => src.WorkTimeType))
            .ForMember(dest => dest.OldLocation, opt => opt.MapFrom((src, dest, destMember, context) =>
            {
                var workTime = MappingHelpers.GetFromContext<WorkTime>(context, MappingKeys.WorkTime);
                return workTime?.Location;
            }))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
        
        CreateMap<WorkTime, WorkTimeLog>()            
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ActionType, opt => opt.MapFrom((src, dest, destMember, context) => MappingHelpers.GetEnumFromContext<ActionType>(context, MappingKeys.ActionType) ?? ActionType.Create))
            .ForMember(dest => dest.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime))
            .ForMember(dest => dest.EndDateTime, opt => opt.MapFrom(src => src.EndDateTime))
            .ForMember(dest => dest.WorkTimeType, opt => opt.MapFrom(src => src.WorkTimeType))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location));
    }
}