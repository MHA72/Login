namespace Login.Models.Customer;

public class Cheque : BaseEntity
{
    public string? CustomerNumber { get; set; }
    public ChequeStatus ChequeStatus { get; set; }
    public string ChequeNumber { get; set; }
    public decimal Amount { get; set; }
    public DateTime ChequeDueDate { get; set; }
}