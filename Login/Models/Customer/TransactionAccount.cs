namespace Login.Models.Customer;

public class TransactionAccount : BaseEntity
{
    public string? AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
}