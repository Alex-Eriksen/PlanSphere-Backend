﻿using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Teams.Commands.CreateTeam;
using PlanSphere.Core.Features.Teams.Request;

namespace PlanSphere.Core.Features.Teams.Profiles;

public class TeamProfile : Profile
{
    public TeamProfile()
    {
        
        CreateMap<CreateTeamCommand, Team>()
            .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Request.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Request.Description))
            .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Request.Identifier))
            .ForMember(dest => dest.InheritAddress, opt => opt.MapFrom(src => src.InheritAddress))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Request.Address));
        CreateMap<TeamPatchRequest, Team>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Identifier, opt => opt.MapFrom(src => src.Identifier))
            .ForMember(dest => dest.InheritAddress, opt => opt.MapFrom(src => src.InheritAddress))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
    }
    
}