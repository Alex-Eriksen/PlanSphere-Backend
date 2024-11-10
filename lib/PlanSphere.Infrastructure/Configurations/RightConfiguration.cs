using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class RightConfiguration : IEntityTypeConfiguration<Right>
{
    public void Configure(EntityTypeBuilder<Right> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasIndex(e => e.Name)
            .IsUnique();
    }
}