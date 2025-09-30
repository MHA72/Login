namespace Login.Models.Customer;

public class Account : BaseEntity
{
    public Customer? Customer { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Amount { get; set; }
    public Guid AverageAccountId { get; set; }
    public AverageAccount? AverageAccount { get; set; }
}