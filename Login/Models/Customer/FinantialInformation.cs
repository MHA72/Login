namespace Login.Models.Customer;

public class FinancialInformation : BaseEntity
{
    public string? CustomerNumber { get; set; }
    public decimal Amount { get; set; }
}