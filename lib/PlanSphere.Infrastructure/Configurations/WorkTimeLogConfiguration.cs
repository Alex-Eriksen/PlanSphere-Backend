using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class WorkTimeLogConfiguration : IEntityTypeConfiguration<WorkTimeLog>
{
    public void Configure(EntityTypeBuilder<WorkTimeLog> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();


        builder.HasOne(e => e.User)
            .WithMany()
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.LoggedByUser)
            .WithMany()
            .HasForeignKey(e => e.LoggedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}