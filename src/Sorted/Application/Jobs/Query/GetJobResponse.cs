public class GetJobResponse
{
    public string Title { get; set; }
    public string OwnerName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Creation { get; set; }
    public decimal AgreedPrice { get; set; }
}