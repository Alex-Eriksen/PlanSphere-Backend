using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.CompanyRole)
            .WithOne(e => e.Role)
            .HasForeignKey<CompanyRole>(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.DepartmentRole)
            .WithOne(e => e.Role)
            .HasForeignKey<DepartmentRole>(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.TeamRole)
            .WithOne(e => e.Role)
            .HasForeignKey<TeamRole>(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.CreatedByUser)
            .WithMany()
            .HasForeignKey(e => e.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(e => e.UpdatedByUser)
            .WithMany()
            .HasForeignKey(e => e.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}