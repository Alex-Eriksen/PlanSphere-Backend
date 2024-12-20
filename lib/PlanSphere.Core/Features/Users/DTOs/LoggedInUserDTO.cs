﻿using PlanSphere.Core.Features.Roles.DTOs;

namespace PlanSphere.Core.Features.Users.DTOs;

public class LoggedInUserDTO
{
    public ulong OrganisationId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string ProfilePictureUrl { get; set; }
    public List<RoleDTO> Roles { get; set; } = new List<RoleDTO>();
}