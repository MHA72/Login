using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Configurations.Customer;

public sealed class AverageAccountConfiguration : IEntityTypeConfiguration<Models.Customer.AverageAccount>
{
    public void Configure(EntityTypeBuilder<Models.Customer.AverageAccount> builder)
    {
        builder.HasKey(customer => customer.Id);
        builder.HasQueryFilter(customer => !customer.IsDeleted);

        builder.Property(customer => customer.AccountAverageMonth).HasPrecision(18, 2);
        builder.Property(customer => customer.AccountAverageThreeMonth).HasPrecision(18, 2);
        builder.Property(customer => customer.AccountAverageSixMonth).HasPrecision(18, 2);
        builder.Property(customer => customer.AccountAverageYear).HasPrecision(18, 2);

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