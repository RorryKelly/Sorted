using Moq;
using Sorted.Application.Query;
using Sorted.Domain;

namespace Tests.Application.Queries;

public class GetInvoiceQueryTests
{
    GetInvoiceHandler getInvoiceHandler;

    Mock<IReadRepository<GetInvoiceQuery, Invoice>> readRepository;

    Mock<IIdentityService> identityService;

    [SetUp]
    public void SetUp()
    {
        identityService = new Mock<IIdentityService>();
        readRepository = new Mock<IReadRepository<GetInvoiceQuery, Invoice>>();
        getInvoiceHandler = new GetInvoiceHandler(readRepository.Object, identityService.Object);
    }

    [Test]
    public async Task InvoiceQueriesShouldReturnCorrectResultAsync()
    {
        string title = "test title";
        decimal agreedPrice = 30.00m;
        string owner = "testUser";
        DateTime creationDate = DateTime.UtcNow.AddDays(1);
        DateTime datePaid = DateTime.UtcNow.AddDays(1);
        Invoice invoice = new InvoiceBuilder()
            .Title(title)
            .Amount(agreedPrice)
            .OwnerId(owner)
            .Creation(creationDate)
            .DatePaid(datePaid)
            .Build();
        GetInvoiceQuery getInvoiceQuery = new GetInvoiceQuery("invoiceId", "jobId", owner, title);
        readRepository.Setup(r => r.FindAll(getInvoiceQuery, CancellationToken.None))
            .ReturnsAsync(Result<List<Invoice>>.Success(new List<Invoice>() { invoice }));
        Access userAccess = new Access() { Read = true, Write = true };
        identityService.Setup(id => id.GetAccess(invoice, CancellationToken.None)).ReturnsAsync(Result<Access>.Success(userAccess));

        List<GetInvoiceResponse> result = await getInvoiceHandler.Handle(getInvoiceQuery, CancellationToken.None);
        GetInvoiceResponse getInvoiceResponse = result.First();

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(getInvoiceResponse.Title, Is.EqualTo(invoice.Title));
        Assert.That(getInvoiceResponse.OwnerId, Is.EqualTo(invoice.OwnerId));
        Assert.That(getInvoiceResponse.DatePaid, Is.EqualTo(invoice.DatePaid));
    }
}