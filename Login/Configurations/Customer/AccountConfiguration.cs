using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Configurations.Customer;

public sealed class AccountConfiguration : IEntityTypeConfiguration<Models.Customer.Account>
{
    public void Configure(EntityTypeBuilder<Models.Customer.Account> builder)
    {
        builder.HasKey(customer => customer.Id);
        builder.HasQueryFilter(customer => !customer.IsDeleted);

        builder.Property(customer => customer.Amount).HasPrecision(18, 2);

        builder.Property(customer => customer.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(user => user.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        
        builder.HasOne(customer => customer.Customer)
            .WithMany()
            .HasForeignKey(customer => customer.CustomerId)
            .IsRequired();  
        
        builder.HasOne(customer => customer.AverageAccount)
            .WithMany()
            .HasForeignKey(customer => customer.AverageAccountId)
            .IsRequired();

    }
}