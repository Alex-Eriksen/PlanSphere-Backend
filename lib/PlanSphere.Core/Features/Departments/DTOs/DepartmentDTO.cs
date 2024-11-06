﻿using PlanSphere.Core.Abstract;
using PlanSphere.Core.Features.Address.DTOs;
using PlanSphere.Core.Interfaces;

namespace PlanSphere.Core.Features.Departments.DTOs;

public class DepartmentDTO : BaseDTO
{
    public string Name { get; set; }
    public string? LogoUrl { get; set; }
    public string Description { get; set; }
    public string Building { get; set; }
    public AddressDTO Address { get; set; }
    
}