using Login.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Configurations.User;

public sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.HasKey(userLogin => userLogin.Id);

        builder.Property(userLogin => userLogin.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(userLogin => userLogin.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());

        builder.HasOne(userLogin => userLogin.User)
            .WithMany(user => user.UserLogins)
            .HasForeignKey(userLogin => userLogin.UserId)
            .IsRequired();

        builder.HasIndex(userLogin => new { userLogin.UserId, userLogin.IsDeleted }).IsUnique();
    }
}