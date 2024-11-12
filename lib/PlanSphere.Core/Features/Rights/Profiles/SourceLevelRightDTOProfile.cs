using AutoMapper;
using Domain.Entities.EmbeddedEntities;
using PlanSphere.Core.Features.Rights.DTOs;

namespace PlanSphere.Core.Features.Rights.Profiles;

public class SourceLevelRightDTOProfile : Profile
{
    public SourceLevelRightDTOProfile()
    {
        CreateMap<List<Right>, SourceLevelRightDTO>()
            .ForMember(dest => dest.HasAdministratorRights, opt => opt.MapFrom(src => src.Contains(Right.Administrator)))
            .ForMember(dest => dest.HasEditRights, opt => opt.MapFrom(src => src.Contains(Right.Edit)))
            .ForMember(dest => dest.HasPureViewRights, opt => opt.MapFrom(src => src.Contains(Right.PureView)))
            .ForMember(dest => dest.HasViewRights, opt => opt.MapFrom(src => src.Contains(Right.View)))
            .ForMember(dest => dest.HasSetOwnWorkScheduleRights, opt => opt.MapFrom(src => src.Contains(Right.SetOwnWorkSchedule)))
            .ForMember(dest => dest.HasSetOwnJobTitleRights, opt => opt.MapFrom(src => src.Contains(Right.SetOwnJobTitle)))
            .ForMember(dest => dest.HasSetAutomaticCheckInOutRights, opt => opt.MapFrom(src => src.Contains(Right.SetAutomaticCheckInOut)))
            .ForMember(dest => dest.HasManuallySetOwnWorkTimeRights, opt => opt.MapFrom(src => src.Contains(Right.ManuallySetOwnWorkTime)))
            .ForMember(dest => dest.HasManageUsersRights, opt => opt.MapFrom(src => src.Contains(Right.ManageUsers)))
            .ForMember(dest => dest.HasManageTimesRights, opt => opt.MapFrom(src => src.Contains(Right.ManageTimes)));
    }
}