using Sorted.Domain;

public static class GetInvoiceResponseMapper
{
    public static GetInvoiceResponse MapToInvoiceResponse(this Invoice invoice)
    {
        return new GetInvoiceResponse()
        {
            Title = invoice.Title,
            OwnerId = invoice.OwnerId,
            IsPaid = invoice.IsPaid,
            Amount = invoice.Amount,
            JobId = invoice.JobId,
            Creation = invoice.Creation,
            DatePaid = invoice.DatePaid
        };
    }
}