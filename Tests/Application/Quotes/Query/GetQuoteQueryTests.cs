using Moq;
using Sorted.Application.Query;
using Sorted.Domain;

namespace Tests.Application.Queries;

public class GetQuoteQueryTests
{
    GetQuoteHandler getQuoteHandler;

    Mock<IReadRepository<GetQuoteQuery, Quote>> readRepository;

    Mock<IIdentityService> identityService;

    [SetUp]
    public void SetUp()
    {
        identityService = new Mock<IIdentityService>();
        readRepository = new Mock<IReadRepository<GetQuoteQuery, Quote>>();
        getQuoteHandler = new GetQuoteHandler(readRepository.Object, identityService.Object);
    }

    [Test]
    public async Task QuoteQueriesShouldReturnCorrectResultAsync()
    {
        const string ownerId = "ownerName";
        const string title = "title";
        DateTime creation = DateTime.MinValue.AddDays(1);
        string jobId = "jobId";
        decimal agreedPrice = 3.0m;
        string userId = "userId";
        Quote quote = new QuoteBuilder()
            .Title(title)
            .AskedPrice(agreedPrice)
            .OwnerId(ownerId)
            .StartDate(creation.AddDays(1))
            .Creation(creation)
            .JobId(jobId)
            .Build();
        GetQuoteQuery getQuoteQuery = new GetQuoteQuery("quoteId", ownerId, title);
        readRepository.Setup(r => r.FindAll(getQuoteQuery, CancellationToken.None))
            .ReturnsAsync(Result<List<Quote>>.Success(new List<Quote>() { quote }));
        Access userAccess = new Access() { Read = true, Write = true };
        identityService.Setup(id => id.GetAccess(quote, CancellationToken.None)).ReturnsAsync(Result<Access>.Success(userAccess));

        List<GetQuoteResponse> result = await getQuoteHandler.Handle(getQuoteQuery, CancellationToken.None);
        GetQuoteResponse getQuoteResponse = result.First();

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(getQuoteResponse.Title, Is.EqualTo(quote.Title));
        Assert.That(getQuoteResponse.OwnerId, Is.EqualTo(quote.OwnerId));
        Assert.That(getQuoteResponse.AskedPrice, Is.EqualTo(quote.AskedPrice));
    }
}