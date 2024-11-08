using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class DepartmentSettingsConfiguration : IEntityTypeConfiguration<DepartmentSettings>
{
    public void Configure(EntityTypeBuilder<DepartmentSettings> builder)
    {
        builder.HasKey(e => e.DepartmentId);
        
        builder.HasOne(e => e.Department)
            .WithOne(e => e.Settings)
            .HasForeignKey<DepartmentSettings>(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.DefaultRole)
            .WithOne()
            .HasForeignKey<DepartmentSettings>(e => e.DefaultRoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.DefaultWorkSchedule)
            .WithOne(e => e.DepartmentSettings)
            .HasForeignKey<DepartmentSettings>(e => e.DefaultWorkScheduleId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}