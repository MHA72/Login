using Login.Configurations.User;
using Login.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Login.Context;

public class LoginContext : DbContext
{
    public LoginContext(DbContextOptions<LoginContext> options) : base(options){}

    public DbSet<User>? Users { get; set; }
    public DbSet<UserLogin>? UserLogins { get; set; }
    public DbSet<FileSheet>? FileSheets { get; set; }
    public DbSet<Sheet>? Sheets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new FileSheetConfiguration());
        modelBuilder.ApplyConfiguration(new SheetConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}