using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class CompanyJobTitleConfiguration : IEntityTypeConfiguration<CompanyJobTitle>
{
    public void Configure(EntityTypeBuilder<CompanyJobTitle> builder)
    {
        builder.HasKey(e => new { e.CompanyId, e.JobTitleId });

        builder.HasOne(e => e.Company)
            .WithMany()
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.JobTitle)
            .WithOne(e => e.CompanyJobTitle)
            .HasForeignKey<CompanyJobTitle>(e => e.JobTitleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}