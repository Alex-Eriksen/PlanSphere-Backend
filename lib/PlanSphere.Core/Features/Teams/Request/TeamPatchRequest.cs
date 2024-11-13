﻿using PlanSphere.Core.Features.Addresses.Requests;

namespace PlanSphere.Core.Features.Teams.Request;

public class TeamPatchRequest
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? Identifier { get; set; }
    public AddressRequest Address { get; set; }
    public bool? InheritDefaultWorkSchedule { get; set; }
}