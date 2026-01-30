using LoanApp.Domain.Entities;
using LoanApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoanApp.Infrastructure.Data.Configurations;

public class LoanApplicationConfiguration : IEntityTypeConfiguration<LoanApplication>
{
    public void Configure(EntityTypeBuilder<LoanApplication> builder)
    {
        builder.ToTable("LoanApplication");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.IdentityToken)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.Title)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(x => x.FirstName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.LastName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(x => x.Mobile)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(e => e.Email)
               .HasMaxLength(256)
               .IsRequired();

        builder.Property(x => x.DateOfBirth)
               .HasColumnType("date")
               .IsRequired();

        builder.Property(x => x.Term)
               .IsRequired();

        builder.Property(m => m.Amount)
               .HasPrecision(18, 2)
               .IsRequired();

        builder.Property(m => m.RepaymentAmount)
               .HasPrecision(18, 2);

        builder.Property(m => m.EstablishmentFee)
               .HasPrecision(18, 2);

        builder.Property(m => m.TotalInterest)
               .HasPrecision(18, 2);

        builder.Property(x => x.Status)
               .HasConversion<string>()
               .HasMaxLength(50)
               .HasDefaultValue(LoanApplicationStatus.Pending);

        builder.Property(x => x.ProductType)
               .HasMaxLength(50);

        builder.Property(x => x.IsActive)
               .HasColumnType("bit")
               .IsRequired()
               .HasDefaultValue(true);

        builder.Property(x => x.ApprovalDate)
               .HasColumnType("datetime2");

        builder.Property(x => x.DateCreated)
               .HasColumnType("datetime2")
               .IsRequired()
               .HasDefaultValue(DateTime.UtcNow);
    }
}
