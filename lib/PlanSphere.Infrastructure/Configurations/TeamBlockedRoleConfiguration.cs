using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class TeamBlockedRoleConfiguration : IEntityTypeConfiguration<TeamBlockedRole>
{
    public void Configure(EntityTypeBuilder<TeamBlockedRole> builder)
    {
        builder.HasKey(e => new { e.TeamId, e.RoleId });

        builder.HasOne(e => e.Team)
            .WithMany()
            .HasForeignKey(e => e.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Role)
            .WithOne()
            .HasForeignKey<TeamBlockedRole>(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}