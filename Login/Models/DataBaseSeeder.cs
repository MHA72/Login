using Login.Context;
using Login.Models.Customer;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(LoginContext context)
    {
        if (context.Customers.Any()) return;

        var customers = new List<Customer>();
        var accounts = new List<Account>();
        var averages = new List<AverageAccount>();
        var cheques = new List<Cheque>();
        var contracts = new List<Contract>();
        var payments = new List<MonthlyPayment>();
        var transactions = new List<TransactionAccount>();
        var financials = new List<FinancialInformation>();

        // ساخت مشتری‌ها
        for (int i = 1; i <= 10; i++)
        {
            var customerNumber = $"CUST{i:000}";
            customers.Add(new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = $"نام{i}",
                LastName = $"نام‌خانوادگی{i}",
                CustomerNumber = customerNumber
            });

            // حساب مرتبط
            var accountNumber = $"ACC{i:000}";
            accounts.Add(new Account
            {
                Id = Guid.NewGuid(),
                CustomerNumber = customerNumber,
                AccountNumber = accountNumber,
                Amount = 10_000_000 + i * 500_000
            });

            // میانگین حساب
            averages.Add(new AverageAccount
            {
                Id = Guid.NewGuid(),
                AccountNumber = accountNumber,
                AccountAverageMonth = 3_000_000 + i * 100_000,
                AccountAverageThreeMonth = 4_000_000 + i * 100_000,
                AccountAverageSixMonth = 5_000_000 + i * 100_000,
                AccountAverageYear = 6_000_000 + i * 100_000
            });

            // چک‌ها
            for (int j = 0; j < 2; j++)
            {
                cheques.Add(new Cheque
                {
                    Id = Guid.NewGuid(),
                    CustomerNumber = customerNumber,
                    ChequeNumber = $"CHQ-{customerNumber}-{j}",
                    Amount = 1_000_000 + j * 500_000,
                    ChequeDueDate = DateTime.Now.AddDays(-j * 30),
                    ChequeStatus = j % 2 == 0 ? ChequeStatus.ClearedCheque : ChequeStatus.BouncedCheque
                });
            }

            // قرارداد
            var contractNumber = $"CN-{customerNumber}";
            contracts.Add(new Contract
            {
                Id = Guid.NewGuid(),
                CustomerNumber = customerNumber,
                ContractNumber = contractNumber,
                LoanBalance = 20_000_000,
                ReminingLoan = i % 3 == 0 ? 0 : 5_000_000,
                FirstDueDate = DateTime.Now.AddMonths(-12),
                LastDueDate = DateTime.Now.AddMonths(6),
                ContractStatus = i % 3 == 0 ? ContractStatus.Done : ContractStatus.Current
            });

            // پرداخت‌های ماهانه
            for (int k = 0; k < 3; k++)
            {
                payments.Add(new MonthlyPayment
                {
                    Id = Guid.NewGuid(),
                    ContractNumber = contractNumber,
                    PaymentDate = DateTime.Now.AddMonths(-k)
                });
            }

            // تراکنش‌ها
            for (int t = 0; t < 5; t++)
            {
                transactions.Add(new TransactionAccount
                {
                    Id = Guid.NewGuid(),
                    AccountNumber = accountNumber,
                    Amount = 500_000 + t * 100_000,
                    TransactionType = t % 2 == 0 ? TransactionType.Credit : TransactionType.Debit
                });
            }

            // اطلاعات مالی
            financials.Add(new FinancialInformation
            {
                Id = Guid.NewGuid(),
                CustomerNumber = customerNumber,
                Amount = 2_000_000 + i * 250_000
            });
        }

        // ذخیره‌سازی مرحله‌ای
        await context.Customers.AddRangeAsync(customers);
        await context.Accounts.AddRangeAsync(accounts);
        await context.AverageAccounts.AddRangeAsync(averages);
        await context.Cheques.AddRangeAsync(cheques);
        await context.Contracts.AddRangeAsync(contracts);
        await context.MonthlyPayments.AddRangeAsync(payments);
        await context.TransactionAccounts.AddRangeAsync(transactions);
        await context.FinancialInformations.AddRangeAsync(financials);

        await context.SaveChangesAsync();
    }
}
