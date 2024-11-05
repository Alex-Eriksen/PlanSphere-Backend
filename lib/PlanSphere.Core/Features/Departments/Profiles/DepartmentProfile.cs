using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Departments.Request;

namespace PlanSphere.Core.Features.Departments.Profiles;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentRequest, Department>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.DepartmentName))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Building))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
    }
}