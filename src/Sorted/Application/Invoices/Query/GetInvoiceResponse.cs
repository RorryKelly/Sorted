public class GetInvoiceResponse
{
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public string OwnerId { get; set; }
    public string JobId { get; set; }
    public DateTime Creation { get; set; }
    public DateTime DatePaid { get; set; }
}