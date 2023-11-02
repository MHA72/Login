using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Configurations.User;

public sealed class UserConfiguration : IEntityTypeConfiguration<Models.User.User>
{
    public void Configure(EntityTypeBuilder<Models.User.User> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasQueryFilter(user => !user.IsDeleted);
        builder.HasIndex(user => user.Username).IsUnique();

        builder.Property(user => user.Username).IsRequired().IsUnicode(false);
        builder.Property(user => user.Email).IsRequired().IsUnicode(false);
        builder.Property(user => user.PasswordHash).IsRequired().IsUnicode(false);

        builder.Property(user => user.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(user => user.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());

    }
}