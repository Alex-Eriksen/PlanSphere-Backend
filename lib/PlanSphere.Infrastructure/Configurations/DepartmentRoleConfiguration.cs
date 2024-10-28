using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class DepartmentRoleConfiguration : IEntityTypeConfiguration<DepartmentRole>
{
    public void Configure(EntityTypeBuilder<DepartmentRole> builder)
    {
        builder.HasKey(e => new { e.DepartmentId, e.RoleId });

        builder.HasOne(e => e.Department)
            .WithMany()
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Role)
            .WithOne(e => e.DepartmentRole)
            .HasForeignKey<DepartmentRole>(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}