using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Departments.DTOs;

namespace PlanSphere.Core.Features.Departments.Profiles;

public class DepartmentLookUpDTOProfile : Profile
{
    public DepartmentLookUpDTOProfile()
    {
        CreateMap<Department, DepartmentLookUpDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name));
    }
}