using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class DepartmentJobTitleConfiguration : IEntityTypeConfiguration<DepartmentJobTitle>
{
    public void Configure(EntityTypeBuilder<DepartmentJobTitle> builder)
    {
        builder.HasKey(e => new { e.DepartmentId, e.JobTitleId });

        builder.HasOne(e => e.Department)
            .WithMany()
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.JobTitle)
            .WithOne(e => e.DepartmentJobTitle)
            .HasForeignKey<DepartmentJobTitle>(e => e.JobTitleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}