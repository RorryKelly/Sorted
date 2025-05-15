using Moq;
using Sorted.Application.Jobs;
using Sorted.Domain;

namespace Tests.Application;

public class CreateQuoteTests()
{
    CreateQuoteCommandHandler createQuote;
    Mock<IQuoteRepository> quoteRepository;
    Mock<IIdentityService> identityService;

    [SetUp]
    public void SetUp()
    {
        quoteRepository = new Mock<IQuoteRepository>();
        identityService = new Mock<IIdentityService>();
        createQuote = new CreateQuoteCommandHandler(quoteRepository.Object, identityService.Object);
    }

    [Test]
    public async Task QuotesShouldBePassedThroughToPersistenceLayer()
    {
        CancellationToken token = new CancellationToken();
        CreateQuoteCommand command = new CreateQuoteCommand(new Guid().ToString(), "Title");
        identityService.Setup(id => id.GetUserId()).Returns(new Guid());
        identityService.Setup(id => id.HasAccess(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        await createQuote.Handle(command, token);

        quoteRepository.Verify(repo => repo.SaveQuote(It.IsAny<Quote>(), token));
    }

    [Test]
    public async Task QuotesShouldGrabInformationOfUserDataToStoreInQuotes()
    {
        string parentId = Guid.NewGuid().ToString();
        string title = "Title";
        CancellationToken token = new CancellationToken();
        CreateQuoteCommand command = new CreateQuoteCommand(parentId, title);
        Guid userId = Guid.NewGuid();
        Quote quote = new Quote();
        quoteRepository.Setup(qRepo => qRepo.SaveQuote(It.IsAny<Quote>(), token))
            .Callback<Quote, CancellationToken>((q, _) => quote = q)
            .ReturnsAsync(userId);
        identityService.Setup(id => id.GetUserId()).Returns(userId);
        identityService.Setup(id => id.HasAccess(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        await createQuote.Handle(command, token);

        quoteRepository.Verify(repo => repo.SaveQuote(It.IsAny<Quote>(), token));
        Assert.That(quote.getUserId(), Is.EqualTo(userId));
    }

    [Test]
    public void QuotesShouldReturnBadResponseWhenUserIsNotFound()
    {
        string parentId = Guid.NewGuid().ToString();
        string title = "Title";
        CancellationToken token = new CancellationToken();
        CreateQuoteCommand command = new CreateQuoteCommand(parentId, title);
        Guid userId = Guid.NewGuid();
        identityService.Setup(id => id.GetUserId()).Throws(new UnauthorizedAccessException("User Not Found"));

        Assert.ThrowsAsync<UnauthorizedAccessException>(() => createQuote.Handle(command, token));
    }

    [Test]
    public void QuotesShouldCheckUserAccess()
    {
        string parentId = Guid.NewGuid().ToString();
        string title = "Title";
        CancellationToken token = new CancellationToken();
        CreateQuoteCommand command = new CreateQuoteCommand(parentId, title);
        Guid userId = Guid.NewGuid();
        identityService.Setup(id => id.HasAccess(parentId, userId.ToString())).Throws(new UnauthorizedAccessException());

        Assert.ThrowsAsync<UnauthorizedAccessException>(() => createQuote.Handle(command, token));
    }
}