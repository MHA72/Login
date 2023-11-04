using Login.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Configurations.User;

public sealed class FileSheetConfiguration : IEntityTypeConfiguration<FileSheet>
{
    public void Configure(EntityTypeBuilder<FileSheet> builder)
    {
        builder.HasKey(user => user.Id);
        builder.HasQueryFilter(user => !user.IsDeleted);

        builder.Property(user => user.UpdateTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());
        builder.Property(user => user.InsertTime)
            .HasConversion(time => time.ToUniversalTime(), time => time.ToUniversalTime());

    }
}