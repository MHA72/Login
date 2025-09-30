namespace Login.Models.Customer;

public class TransactionAccount : BaseEntity
{
    public Guid AccountId { get; set; }
    public Account? Account { get; set; }
    public decimal Amount { get; set; }
    public TransactionType TransactionType { get; set; }
}