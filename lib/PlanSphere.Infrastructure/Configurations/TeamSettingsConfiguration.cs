using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class TeamSettingsConfiguration : IEntityTypeConfiguration<TeamSettings>
{
    public void Configure(EntityTypeBuilder<TeamSettings> builder)
    {
        builder.HasKey(e => e.TeamId);
        
        builder.HasOne(e => e.Team)
            .WithOne(e => e.Settings)
            .HasForeignKey<TeamSettings>(e => e.TeamId)
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