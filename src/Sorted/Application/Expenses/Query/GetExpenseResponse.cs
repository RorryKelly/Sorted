public class GetExpenseResponse
{
    public string Title { get; set; }
    public string OwnerId { get; set; }
    public string JobId { get; set; }
    public DateTime Creation { get; set; }
    public decimal Cost { get; set; }
    public int Quantity { get; set; }
}