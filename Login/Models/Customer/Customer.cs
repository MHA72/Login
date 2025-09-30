namespace Login.Models.Customer;

public class Customer : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CustomerNumber { get; set; }
}