using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class DepartmentBlockedRoleConfiguration : IEntityTypeConfiguration<DepartmentBlockedRole>
{
    public void Configure(EntityTypeBuilder<DepartmentBlockedRole> builder)
    {
        builder.HasKey(e => new { e.DepartmentId, e.RoleId });

        builder.HasOne(e => e.Department)
            .WithMany()
            .HasForeignKey(e => e.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Role)
            .WithMany(e => e.BlockedDepartments)
            .HasForeignKey(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}