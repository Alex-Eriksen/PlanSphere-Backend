using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class CompanyBlockedRoleConfiguration : IEntityTypeConfiguration<CompanyBlockedRole>
{
    public void Configure(EntityTypeBuilder<CompanyBlockedRole> builder)
    {
        builder.HasKey(e => new { e.CompanyId, e.RoleId });

        builder.HasOne(e => e.Company)
            .WithMany()
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Role)
            .WithOne()
            .HasForeignKey<CompanyBlockedRole>(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}