using Moq;
using Sorted.Application.Commands;
using Sorted.Domain;

namespace Tests.Application.Commands;

public class InvoiceTests
{
    CreateInvoiceHandler createInvoiceHandler;
    Mock<ICreateRepository<Invoice, string>> repository;
    Mock<IIdentityService> identityService;

    [SetUp]
    public void Setup()
    {
        identityService = new Mock<IIdentityService>();
        repository = new Mock<ICreateRepository<Invoice, string>>();
        createInvoiceHandler = new CreateInvoiceHandler(repository.Object, identityService.Object);
    }

    [Test]
    public async Task InvoicesCreationShouldCallPersistenceLayerAsync()
    {
        string jobId = "job id";
        const string ownerId = "ownerName";
        const string title = "title";
        DateTime startDate = DateTime.MinValue.AddDays(1);
        DateTime datePaid = DateTime.MinValue.AddDays(2);
        DateTime creation = DateTime.MinValue.AddDays(1);
        decimal amount = 3.0m;
        string userId = "userId";
        repository.Setup(r => r.Create(It.IsAny<Invoice>(), CancellationToken.None)).ReturnsAsync(Result<string>.Success("Success"));
        identityService.Setup(id => id.GetUserId()).ReturnsAsync(Result<string>.Success(userId));
        CreateInvoiceCommand createInvoiceHandlerCommand = new CreateInvoiceCommand(title, ownerId, jobId, startDate, datePaid, creation, amount);

        await createInvoiceHandler.Handle(createInvoiceHandlerCommand, CancellationToken.None);

        repository.Verify(r => r.Create(It.IsAny<Invoice>(), CancellationToken.None));
    }
}