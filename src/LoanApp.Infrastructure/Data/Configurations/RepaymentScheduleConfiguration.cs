using LoanApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanApp.Infrastructure.Data.Configurations;

public class RepaymentScheduleConfiguration : IEntityTypeConfiguration<RepaymentSchedule>
{
    public void Configure(EntityTypeBuilder<RepaymentSchedule> builder)
    { 
        builder.ToTable("RepaymentSchedule");
        builder.HasKey(x => x.Id); builder.Property(x => x.LoanApplicationId);

        builder.Property(x => x.InstallmentNumber);

        builder.Property(x => x.DueDate);

        builder.Property(x => x.RepaymentAmount)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Principal)
            .HasColumnType("decimal(18,2)");

        builder.Property(x => x.PaidAmount)
            .HasColumnType("decimal(18,2)");

        builder.HasIndex(x => new { x.LoanApplicationId, x.InstallmentNumber })
            .IsUnique();
    }
}
