using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class TeamRoleConfiguration : IEntityTypeConfiguration<TeamRole>
{
    public void Configure(EntityTypeBuilder<TeamRole> builder)
    {
        builder.HasKey(e => new { e.TeamId, e.RoleId });

        builder.HasOne(e => e.Team)
            .WithMany(e => e.Roles)
            .HasForeignKey(e => e.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Role)
            .WithOne(e => e.TeamRole)
            .HasForeignKey<TeamRole>(e => e.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}