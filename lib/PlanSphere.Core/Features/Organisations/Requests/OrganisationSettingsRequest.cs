﻿namespace PlanSphere.Core.Features.Organisations.Requests;

public class OrganisationSettingsRequest
{
    public ulong DefaultRoleId { get; set; }
    public ulong DefaultWorkScheduleId { get; set; }
}