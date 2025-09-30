using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Configurations.Customer;

public sealed class TransactionAccountConfiguration : IEntityTypeConfiguration<Models.Customer.TransactionAccount>
{
    public void Configure(EntityTypeBuilder<Models.Customer.TransactionAccount> builder)
    {
        builder.HasKey(customer => customer.Id);
        builder.HasQueryFilter(customer => !customer.IsDeleted);

        builder.Property(customer => customer.Amount).HasPrecision(18, 2);

        builder.Property(customer => customer.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(user => user.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
    }
}