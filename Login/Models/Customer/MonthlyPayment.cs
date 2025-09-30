namespace Login.Models.Customer;

public class MonthlyPayment : BaseEntity
{
    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }
    public DateTime PaymentDate { get; set; }
}