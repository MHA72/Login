namespace Login.Models.Customer;

public class MonthlyPayment : BaseEntity
{
    public string? ContractNumber { get; set; }
    public DateTime PaymentDate { get; set; }
}