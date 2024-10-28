using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        builder.HasKey(e => e.UserId);

        builder.HasOne(e => e.User)
            .WithOne(e => e.Settings)
            .HasForeignKey<UserSettings>(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.WorkSchedule)
            .WithOne()
            .HasForeignKey<UserSettings>(e => e.WorkScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}