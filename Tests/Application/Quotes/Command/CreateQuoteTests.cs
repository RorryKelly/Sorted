using Microsoft.AspNetCore.Builder;
using Moq;
using Sorted.Application.Commands;
using Sorted.Domain;

namespace Tests.Application.Commands;

public class QuoteTests
{
    CreateQuoteHandler createQuoteHandler;
    Mock<ICreateRepository<Quote, string>> repository;
    Mock<IIdentityService> identityService;

    [SetUp]
    public void Setup()
    {
        identityService = new Mock<IIdentityService>();
        repository = new Mock<ICreateRepository<Quote, string>>();
        createQuoteHandler = new CreateQuoteHandler(repository.Object, identityService.Object);
    }

    [Test]
    public async Task QuotesCreationShouldCallPersistenceLayerAsync()
    {
        const string ownerId = "ownerName";
        const string title = "title";
        DateTime creation = DateTime.MinValue.AddDays(1);
        string jobId = "jobId";
        decimal agreedPrice = 3.0m;
        string userId = "userId";
        repository.Setup(r => r.Create(It.IsAny<Quote>(), CancellationToken.None)).ReturnsAsync(Result<string>.Success("Success"));
        identityService.Setup(id => id.GetUserId()).ReturnsAsync(Result<string>.Success(userId));
        CreateQuoteCommand createQuoteHandlerCommand = new CreateQuoteCommand(title, ownerId, creation, jobId, agreedPrice);

        await createQuoteHandler.Handle(createQuoteHandlerCommand, CancellationToken.None);

        repository.Verify(r => r.Create(It.IsAny<Quote>(), CancellationToken.None));
    }
}