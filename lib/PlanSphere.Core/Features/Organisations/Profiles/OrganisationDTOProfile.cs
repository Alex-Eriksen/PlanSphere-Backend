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
            .ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users.Count))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Count))
            .ForMember(dest => dest.OrganisationMembers, opt =>
            {
                opt.MapFrom(src => src.Users.SelectMany(x => x.Roles).Count(x => x.Role.OrganisationRole != null));
            })
            .ForMember(dest => dest.CompanyMembers, opt =>
            {
                opt.MapFrom(src => src.Users.SelectMany(x => x.Roles).Count(x => x.Role.CompanyRole != null));
            })
            .ForMember(dest => dest.DepartmentMembers, opt =>
            {
                opt.MapFrom(src => src.Users.SelectMany(x => x.Roles).Count(x => x.Role.DepartmentRole != null));
            })
            .ForMember(dest => dest.TeamsMembers, opt =>
            {
                opt.MapFrom(src => src.Users.SelectMany(x => x.Roles).Count(x => x.Role.TeamRole != null));
            });
    }
}