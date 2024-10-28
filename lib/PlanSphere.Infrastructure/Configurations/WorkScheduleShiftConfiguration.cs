using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PlanSphere.Infrastructure.Configurations;

public class WorkScheduleShiftConfiguration : IEntityTypeConfiguration<WorkScheduleShift>
{
    public void Configure(EntityTypeBuilder<WorkScheduleShift> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(e => e.WorkSchedule)
            .WithMany(e => e.WorkScheduleShifts)
            .HasForeignKey(e => e.WorkScheduleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}