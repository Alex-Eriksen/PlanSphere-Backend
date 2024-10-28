﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class OrganisationSettingsConfiguration : IEntityTypeConfiguration<OrganisationSettings>
{
    public void Configure(EntityTypeBuilder<OrganisationSettings> builder)
    {
        builder.HasKey(e => e.OrganisationId);
        
        builder.HasOne(e => e.Organisation)
            .WithOne(e => e.Settings)
            .HasForeignKey<OrganisationSettings>(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.DefaultRole)
            .WithMany()
            .HasForeignKey(e => e.DefaultRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.DefaultWorkSchedule)
            .WithMany()
            .HasForeignKey(e => e.DefaultWorkScheduleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}