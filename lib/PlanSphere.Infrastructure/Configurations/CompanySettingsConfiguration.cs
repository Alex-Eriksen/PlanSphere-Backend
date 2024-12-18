﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class CompanySettingsConfiguration : IEntityTypeConfiguration<CompanySettings>
{
    public void Configure(EntityTypeBuilder<CompanySettings> builder)
    {
        builder.HasKey(e => e.CompanyId);
        
        builder.HasOne(e => e.Company)
            .WithOne(e => e.Settings)
            .HasForeignKey<CompanySettings>(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.DefaultRole)
            .WithOne()
            .HasForeignKey<CompanySettings>(e => e.DefaultRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.DefaultWorkSchedule)
            .WithOne(e => e.CompanySettings)
            .HasForeignKey<CompanySettings>(e => e.DefaultWorkScheduleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}