namespace Login.Models.Customer;

public class Cheque : BaseEntity
{
    public Customer? Customer { get; set; }
    public Guid CustomerId { get; set; }
    public ChequeStatus ChequeStatus { get; set; }
    public string ChequeNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime ChequeDueDate { get; set; }
}