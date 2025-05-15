using Sorted.Domain;

public interface IQuoteRepository
{
    Task<Guid> SaveQuote(Quote quote, CancellationToken token);
}