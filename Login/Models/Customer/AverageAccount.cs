namespace Login.Models.Customer;

public class AverageAccount : BaseEntity
{
    public string? AccountNumber { get; set; }
    public decimal AccountAverageYear { get; set; }
    public decimal AccountAverageMonth { get; set; }
    public decimal AccountAverageSixMonth { get; set; }
    public decimal AccountAverageThreeMonth { get; set; }
}