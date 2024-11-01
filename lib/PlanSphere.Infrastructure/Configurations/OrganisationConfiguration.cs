using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class OrganisationConfiguration : IEntityTypeConfiguration<Organisation>
{
    public void Configure(EntityTypeBuilder<Organisation> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasMany(o => o.Users)
            .WithOne(u => u.Organisation)
            .HasForeignKey(u => u.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.Address)
            .WithOne()
            .HasForeignKey<Organisation>(e => e.AddressId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.CreatedByUser)
            .WithMany()
            .HasForeignKey(e => e.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(e => e.UpdatedByUser)
            .WithMany()
            .HasForeignKey(e => e.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(e => e.Settings)
            .WithOne(e => e.Organisation)
            .HasForeignKey<Organisation>(e => e.SettingsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}