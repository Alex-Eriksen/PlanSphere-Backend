using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class OrganisationRoleRightConfiguration : IEntityTypeConfiguration<OrganisationRoleRight>
{
    public void Configure(EntityTypeBuilder<OrganisationRoleRight> builder)
    {
        builder.HasKey(e => e.OrganisationId);

        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Role)
            .WithMany(e => e.OrganisationRoleRights)
            .HasForeignKey(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Right)
            .WithMany()
            .HasForeignKey(e => e.RightId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}