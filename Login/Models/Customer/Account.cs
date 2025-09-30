namespace Login.Models.Customer;

public class Account : BaseEntity
{
    public string CustomerNumber { get; set; }
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
}