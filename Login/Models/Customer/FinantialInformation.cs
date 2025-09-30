namespace Login.Models.Customer;

public class FinancialInformation : BaseEntity
{
    public Customer? Customer { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Amount { get; set; }
}