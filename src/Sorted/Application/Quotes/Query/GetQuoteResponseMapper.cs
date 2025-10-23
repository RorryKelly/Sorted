using Sorted.Domain;

public static class GetQuoteResponseMapper
{
    public static GetQuoteResponse MapToQuoteResponse(this Quote quote)
    {
        return new GetQuoteResponse()
        {
            Title = quote.Title,
            OwnerId = quote.OwnerId,
            Creation = quote.Creation,
            JobId = quote.JobId,
            AskedPrice = quote.AskedPrice
        };
    }
}