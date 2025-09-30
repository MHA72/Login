using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Configurations.Customer;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Models.Customer.Customer>
{
    public void Configure(EntityTypeBuilder<Models.Customer.Customer> builder)
    {
        builder.HasKey(customer => customer.Id);
        builder.HasQueryFilter(customer => !customer.IsDeleted);

        builder.Property(customer => customer.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(customer => customer.LastName).IsRequired().HasMaxLength(100);
        builder.Property(customer => customer.CustomerNumber).IsRequired().HasMaxLength(300);

        builder.Property(customer => customer.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(user => user.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        
        builder.HasOne(customer => customer.Account)
            .WithMany()
            .HasForeignKey(customer => customer.AccountId)
            .IsRequired();

    }
}