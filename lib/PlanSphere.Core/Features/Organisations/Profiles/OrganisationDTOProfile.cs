using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Organisations.DTOs;

namespace PlanSphere.Core.Features.Organisations.Profiles;

public class OrganisationDTOProfile : Profile
{
    public OrganisationDTOProfile()
    {
        CreateMap<Organisation, OrganisationDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.LogoUrl, opt => opt.MapFrom(src => src.LogoUrl))
            .ForMember(dest => dest.OrganisationMembers, opt => opt.MapFrom(src => src.Users.SelectMany(r => r.Roles.Select(or => or.Role.OrganisationRole)).Count()))
            .ForMember(dest => dest.CompanyMembers, opt => opt.MapFrom(src => src.Users.SelectMany(r => r.Roles.Select(or => or.Role.CompanyRole)).Count()))
            .ForMember(dest => dest.DepartmentMembers, opt => opt.MapFrom(src => src.Users.SelectMany(r => r.Roles.Select(or => or.Role.DepartmentRole)).Count()))
            .ForMember(dest => dest.TeamsMembers, opt => opt.MapFrom(src => src.Users.SelectMany(r => r.Roles.Select(or => or.Role.TeamRole)).Count()));
    }
}