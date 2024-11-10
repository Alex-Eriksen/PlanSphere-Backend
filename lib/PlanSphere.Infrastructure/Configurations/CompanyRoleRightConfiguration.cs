using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class CompanyRoleRightConfiguration : IEntityTypeConfiguration<CompanyRoleRight>
{
    public void Configure(EntityTypeBuilder<CompanyRoleRight> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.Company)
            .WithMany()
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Role)
            .WithMany(e => e.CompanyRoleRights)
            .HasForeignKey(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Right)
            .WithMany()
            .HasForeignKey(e => e.RightId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}