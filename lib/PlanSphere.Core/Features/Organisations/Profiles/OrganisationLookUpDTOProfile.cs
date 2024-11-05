using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Organisations.DTOs;

namespace PlanSphere.Core.Features.Organisations.Profiles;

public class OrganisationLookUpDTOProfile : Profile
{
    public OrganisationLookUpDTOProfile()
    {
        CreateMap<Organisation, OrganisationLookUpDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));
    }
}