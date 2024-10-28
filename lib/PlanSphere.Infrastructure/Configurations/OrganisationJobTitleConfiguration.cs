using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class OrganisationJobTitleConfiguration : IEntityTypeConfiguration<OrganisationJobTitle>
{
    public void Configure(EntityTypeBuilder<OrganisationJobTitle> builder)
    {
        builder.HasKey(e => new { e.OrganisationId, e.JobTitleId });

        builder.HasOne(e => e.Organisation)
            .WithMany()
            .HasForeignKey(e => e.OrganisationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.JobTitle)
            .WithOne(e => e.OrganisationJobTitle)
            .HasForeignKey<OrganisationJobTitle>(e => e.JobTitleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}