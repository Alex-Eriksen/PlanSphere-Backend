using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class UserJobTitlesConfiguration : IEntityTypeConfiguration<UserJobTitle>
{
    public void Configure(EntityTypeBuilder<UserJobTitle> builder)
    {
        builder.HasKey(e => new { e.UserId, e.JobTitleId });

        builder.HasOne(e => e.User)
            .WithMany(e => e.JobTitles)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.JobTitle)
            .WithMany()
            .HasForeignKey(e => e.JobTitleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}