



namespace Sorted.Domain;

public class Trader
{
    private List<Job> _jobs;
    private string username;
    private List<Quote> _quotes;

    public Trader()
    {
    }

    public Trader(string username)
    {
        this.username = username;
    }

    public List<Quote> getPendingQuotes()
    {
        if (_quotes == null)
        {
            _quotes = new List<Quote>();
        }

        return _quotes;
    }

    public Quote createQuote(string title)
    {
        if (_quotes == null)
        {
            _quotes = new List<Quote>();
        }

        Quote quote = new Quote(username, title);
        _quotes.Add(quote);

        return quote;
    }

    public List<Job> getJobs()
    {
        if (_jobs == null)
        {
            _jobs = new List<Job>();
        }

        return _jobs;
    }

    public Job createJob(Quote quote, DateTime startDate, DateTime endDate, decimal agreedPrice)
    {
        if (_jobs == null)
        {
            _jobs = new List<Job>();
        }
        Job job = quote.Accept(startDate, endDate, agreedPrice);
        _quotes.Remove(quote);
        _jobs.Add(job);

        return job;
    }
}