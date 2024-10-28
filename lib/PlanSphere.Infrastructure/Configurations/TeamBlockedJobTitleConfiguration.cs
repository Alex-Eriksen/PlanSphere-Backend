﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class TeamBlockedJobTitleConfiguration : IEntityTypeConfiguration<TeamBlockedJobTitle>
{
    public void Configure(EntityTypeBuilder<TeamBlockedJobTitle> builder)
    {
        builder.HasKey(e => new { e.TeamId, e.JobTitleId });

        builder.HasOne(e => e.Team)
            .WithMany()
            .HasForeignKey(e => e.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.JobTitle)
            .WithOne()
            .HasForeignKey<TeamBlockedJobTitle>(e => e.JobTitleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}