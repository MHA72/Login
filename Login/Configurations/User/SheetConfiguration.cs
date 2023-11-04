using Login.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Configurations.User;

public sealed class SheetConfiguration : IEntityTypeConfiguration<Sheet>
{
    public void Configure(EntityTypeBuilder<Sheet> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasQueryFilter(user => !user.IsDeleted);

        builder.Property(user => user.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(user => user.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());

    }
}