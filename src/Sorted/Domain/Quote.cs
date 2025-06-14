

using Sorted.Domain.ValueObject;

namespace Sorted.Domain;

public class Quote : IDomainAccess
{
    public string OwnerId { get; set; }
    public string Title { get; set; }
    public DateTime Creation { get; set; }
    public string JobId { get; set; }
    public decimal AskedPrice { get; set; }
}

public class QuoteBuilder
{
    private Quote _quote;

    public QuoteBuilder()
    {
        _quote = new Quote();
    }

    public QuoteBuilder Title(string title)
    {
        _quote.Title = title;
        return this;
    }

    public QuoteBuilder OwnerId(string ownerId)
    {
        _quote.OwnerId = ownerId;
        return this;
    }

    public QuoteBuilder StartDate(DateTime creation)
    {
        _quote.Creation = creation;
        return this;
    }

    public QuoteBuilder JobId(string jobId)
    {
        _quote.JobId = jobId;
        return this;
    }

    public QuoteBuilder Creation(DateTime creation)
    {
        _quote.Creation = creation;
        return this;
    }

    public QuoteBuilder AskedPrice(decimal askedPrice)
    {
        _quote.AskedPrice = askedPrice;
        return this;
    }

    public Quote Build()
    {
        if (_quote.Creation == DateTime.MinValue)
        {
            _quote.Creation = DateTime.UtcNow;
        }
        return _quote;
    }
}