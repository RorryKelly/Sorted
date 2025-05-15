using Sorted.Domain;
using Sorted.Domain.ValueObject;
using Sorted.Application.Jobs;

namespace Tests.Domain;

public class QuoteTests
{
    [Test]
    public void QuotesShouldBelongToSomeoneAndHaveATitle()
    {
        Guid parentId = new Guid();
        string title = "New Boiler Quote";

        Quote quote = new Quote(new CreateQuoteCommand(parentId.ToString(), title));

        Assert.That(quote.getParentId(), Is.EqualTo(parentId));
        Assert.That(quote.getTitle(), Is.EqualTo(title));
    }

    [Test]
    public void QuotesShouldKeepTrackOfCreationDate()
    {
        Guid parentId = new Guid();
        string title = "New Boiler Quote";

        Quote quote = new Quote(new CreateQuoteCommand(parentId.ToString(), title));

        Assert.That(DateTime.UtcNow.CompareTo(quote.getCreation()), Is.AtLeast(0));
    }
}
