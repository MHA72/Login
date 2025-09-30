using Login.Configurations.Customer;
using Login.Configurations.User;
using Login.Models.Customer;
using Login.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Login.Context;

public class LoginContext : DbContext
{
    public LoginContext(DbContextOptions<LoginContext> options) : base(options){}

    public DbSet<User>? Users { get; set; }
    public DbSet<Cheque>? Cheques { get; set; }
    public DbSet<Account>? Accounts { get; set; }
    public DbSet<Customer>? Customers { get; set; }
    public DbSet<UserLogin>? UserLogins { get; set; }
    public DbSet<AverageAccount>? AverageAccounts { get; set; }
    public DbSet<MonthlyPayment>? MonthlyPayments { get; set; }
    public DbSet<TransactionAccount>? TransactionAccounts { get; set; }
    public DbSet<FinancialInformation>? FinancialInformations { get; set; }
    public DbSet<Contract>? Contracts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserLoginConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        modelBuilder.ApplyConfiguration(new AverageAccountConfiguration());
        modelBuilder.ApplyConfiguration(new ChequeConfiguration());
        modelBuilder.ApplyConfiguration(new TransactionAccountConfiguration());
        modelBuilder.ApplyConfiguration(new MonthlyPaymentConfiguration());
        modelBuilder.ApplyConfiguration(new FinancialInformationConfiguration());
        modelBuilder.ApplyConfiguration(new ContractConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
    }
}