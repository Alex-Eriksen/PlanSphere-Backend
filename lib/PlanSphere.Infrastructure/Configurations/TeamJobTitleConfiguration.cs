using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class TeamJobTitleConfiguration : IEntityTypeConfiguration<TeamJobTitle>
{
    public void Configure(EntityTypeBuilder<TeamJobTitle> builder)
    {
        builder.HasKey(e => new { e.TeamId, e.JobTitleId });

        builder.HasOne(e => e.Team)
            .WithMany()
            .HasForeignKey(e => e.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.JobTitle)
            .WithOne(e => e.TeamJobTitle)
            .HasForeignKey<TeamJobTitle>(e => e.JobTitleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}