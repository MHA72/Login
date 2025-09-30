using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Configurations.Customer;

public sealed class ChequeConfiguration : IEntityTypeConfiguration<Models.Customer.Cheque>
{
    public void Configure(EntityTypeBuilder<Models.Customer.Cheque> builder)
    {
        builder.HasKey(cheque => cheque.Id);
        builder.HasQueryFilter(cheque => !cheque.IsDeleted);

        builder.Property(cheque => cheque.Amount).HasPrecision(18, 2);
        builder.Property(cheque => cheque.ChequeNumber).HasMaxLength(100);

        builder.Property(cheque => cheque.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(cheque => cheque.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(cheque => cheque.ChequeDueDate)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
    }
}