using AutoMapper;
using Domain.Entities;
using PlanSphere.Core.Features.Departments.Commands.CreateDepartment;
using PlanSphere.Core.Features.Departments.Request;

namespace PlanSphere.Core.Features.Departments.Profiles;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<DepartmentRequest, Department>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Building))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.InheritAddress, opt => opt.MapFrom(src => src.InheritAddress));
        CreateMap<CreateDepartmentCommand, Department>()
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Request.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Request.Description))
            .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Request.Building))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Request.Address))
            .ForMember(dest => dest.InheritAddress, opt => opt.MapFrom(src => src.InheritAddress));

        CreateMap<DepartmentUpdateRequest, Department>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.Building, opt => opt.MapFrom(src => src.Building))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.InheritAddress, opt => opt.MapFrom(src => src.InheritAddress))
            .ForPath(dest => dest.Settings.InheritDefaultWorkSchedule, opt => opt.MapFrom(src => src.InheritDefaultWorkSchedule));
    }
}