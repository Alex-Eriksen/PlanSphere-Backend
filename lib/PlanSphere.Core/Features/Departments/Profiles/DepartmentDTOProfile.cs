using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Departments.DTOs;

namespace PlanSphere.Core.Features.Departments.Profiles;

public class DepartmentDTOProfile : Profile
{
    public DepartmentDTOProfile()
    {
        CreateMap<Department, DepartmentDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Building))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));
    }
}