using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Configurations.Customer;

public sealed class ContractConfiguration : IEntityTypeConfiguration<Models.Customer.Contract>
{
    public void Configure(EntityTypeBuilder<Models.Customer.Contract> builder)
    {
        builder.HasKey(customer => customer.Id);
        builder.HasQueryFilter(customer => !customer.IsDeleted);

        
        builder.Property(customer => customer.ContractNumber).IsRequired().HasMaxLength(100);
        builder.Property(customer => customer.LoanBalance).HasPrecision(18, 2);
        builder.Property(customer => customer.ReminingLoan).HasPrecision(18, 2);

        builder.Property(customer => customer.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(user => user.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(user => user.FirstDueDate)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(user => user.LastDueDate)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
    }
}