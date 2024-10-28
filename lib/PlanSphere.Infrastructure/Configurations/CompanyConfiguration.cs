using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.Organisation)
            .WithMany(e => e.Companies)
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(e => e.Address)
            .WithOne()
            .HasForeignKey<Company>(e => e.AddressId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.CreatedByUser)
            .WithMany()
            .HasForeignKey(e => e.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(e => e.UpdatedByUser)
            .WithMany()
            .HasForeignKey(e => e.UpdatedBy)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(e => e.Settings)
            .WithOne(e => e.Company)
            .HasForeignKey<Company>(e => e.SettingsId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}