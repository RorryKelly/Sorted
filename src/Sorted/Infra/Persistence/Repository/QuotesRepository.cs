using Sorted.Domain;

public class QuoteRepository : ICreateRepository<Quote, string>, IReadRepository<GetQuoteQuery, Quote>
{
    public Task<Result<string>> Create(Quote payload, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Quote>>> FindAll(GetQuoteQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Quote>> FindFirst(GetQuoteQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}