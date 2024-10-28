using Domain.Entities;
using AutoMapper;
using PlanSphere.Core.Features.Addresses.Requests;

namespace PlanSphere.Core.Features.Profiles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<AddressRequest, Address>();
    }
}