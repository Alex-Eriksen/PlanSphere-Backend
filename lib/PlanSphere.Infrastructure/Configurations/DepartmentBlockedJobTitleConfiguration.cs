using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class DepartmentBlockedJobTitleConfiguration : IEntityTypeConfiguration<DepartmentBlockedJobTitle>
{
    public void Configure(EntityTypeBuilder<DepartmentBlockedJobTitle> builder)
    {
        builder.HasKey(e => new { e.DepartmentId, e.JobTitleId });

        builder.HasOne(e => e.Department)
            .WithMany()
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.JobTitle)
            .WithOne()
            .HasForeignKey<DepartmentBlockedJobTitle>(e => e.JobTitleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}