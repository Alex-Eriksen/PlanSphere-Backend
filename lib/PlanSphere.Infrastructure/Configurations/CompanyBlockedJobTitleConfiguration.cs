using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class CompanyBlockedJobTitleConfiguration : IEntityTypeConfiguration<CompanyBlockedJobTitle>
{
    public void Configure(EntityTypeBuilder<CompanyBlockedJobTitle> builder)
    {
        builder.HasKey(e => new { e.CompanyId, e.JobTitleId });

        builder.HasOne(e => e.Company)
            .WithMany()
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.JobTitle)
            .WithMany(j => j.CompanyBlockedJobTitles)
            .HasForeignKey(e => e.JobTitleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}