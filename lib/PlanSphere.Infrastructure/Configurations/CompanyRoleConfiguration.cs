using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class CompanyRoleConfiguration : IEntityTypeConfiguration<CompanyRole>
{
    public void Configure(EntityTypeBuilder<CompanyRole> builder)
    {
        builder.HasKey(e => new { e.CompanyId, e.RoleId });

        builder.HasOne(e => e.Company)
            .WithMany()
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Role)
            .WithOne(e => e.CompanyRole)
            .HasForeignKey<CompanyRole>(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}