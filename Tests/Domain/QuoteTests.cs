using Sorted.Domain;
using Sorted.Domain.ValueObject;

namespace Tests.Domain;

public class QuoteTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void QuotesShouldBeBuildable()
    {
        string title = "test title";
        decimal agreedPrice = 30.00m;
        string owner = "testUser";
        DateTime creationDate = DateTime.UtcNow.AddDays(1);

        QuoteBuilder QuoteBuilder = new QuoteBuilder();
        Quote newQuote = QuoteBuilder
            .Title(title)
            .AskedPrice(agreedPrice)
            .OwnerId(owner)
            .Creation(creationDate)
            .Build();

        Assert.That(newQuote.Title, Is.EqualTo(title));
        Assert.That(newQuote.AskedPrice, Is.EqualTo(agreedPrice));
        Assert.That(newQuote.OwnerId, Is.EqualTo(owner));
        Assert.That(newQuote.Creation, Is.EqualTo(creationDate));
    }
}