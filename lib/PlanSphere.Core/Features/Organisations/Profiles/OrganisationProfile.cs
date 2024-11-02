using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Organisations.Commands.CreateOrganisation;
using PlanSphere.Core.Features.Organisations.Commands.UpdateOrganisation;

namespace PlanSphere.Core.Features.Organisations.Profiles;

public class OrganisationProfile : Profile
{
    public OrganisationProfile()
    {
        CreateMap<CreateOrganisationCommand, Organisation>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OrganisationName))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<UpdateOrganisationCommand, Organisation>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.OrganisationUpdateRequest.Name));
    }
}