using Sorted.Domain;
using Sorted.Domain.ValueObject;

namespace Tests.Domain;

public class TraderTests
{
    [Test]
    public void TraderShouldBeAbleToCreateNewQuote()
    {
        string username = "testUser";
        string quoteTitle = "testQuote";
        Trader trader = new Trader(username);

        Quote quote = trader.createQuote(quoteTitle);

        Assert.That(quote.getOwnerName(), Is.EqualTo(username));
        Assert.That(trader.getPendingQuotes().Count(), Is.EqualTo(1));
    }

    [Test]
    public void TraderShouldBeAbleToCreateNewJobsFromQuotes()
    {
        string username = "testUser";
        string quoteTitle = "testQuote";
        Trader trader = new Trader(username);
        Quote quote = trader.createQuote(quoteTitle);

        Job job = trader.createJob(quote, DateTime.Now, DateTime.Now.AddDays(3), 30.00m);

        List<Job> jobs = trader.getJobs();
        Assert.That(jobs.Count(), Is.EqualTo(1));
    }
}