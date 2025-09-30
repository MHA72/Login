namespace Login.Models.Customer;

public class Contract : BaseEntity
{
    public Customer? Customer { get; set; }
    public Guid CustomerId { get; set; }
    public string ContractNumber { get; set; }
    public decimal LoanBalance { get; set; }
    public decimal ReminingLoan { get; set; }
    public DateTime FirstDueDate { get; set; }
    public DateTime LastDueDate { get; set; }
    public ContractStatus ContractStatus { get; set; }
}