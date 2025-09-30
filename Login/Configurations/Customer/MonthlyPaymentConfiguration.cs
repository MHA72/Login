using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Configurations.Customer;

public sealed class MonthlyPaymentConfiguration : IEntityTypeConfiguration<Models.Customer.MonthlyPayment>
{
    public void Configure(EntityTypeBuilder<Models.Customer.MonthlyPayment> builder)
    {
        builder.HasKey(customer => customer.Id);
        builder.HasQueryFilter(customer => !customer.IsDeleted);

        builder.Property(customer => customer.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(user => user.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(user => user.PaymentDate)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());

        builder.HasOne(customer => customer.Contract)
            .WithMany()
            .HasForeignKey(customer => customer.ContractId)
            .IsRequired();
    }
}