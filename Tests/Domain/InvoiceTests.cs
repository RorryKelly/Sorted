using Sorted.Domain;
using Sorted.Domain.ValueObject;

namespace Tests.Domain;

public class InvoiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void InvoicesShouldBeBuildable()
    {
        string title = "test title";
        decimal agreedPrice = 30.00m;
        string owner = "testUser";
        DateTime creationDate = DateTime.UtcNow.AddDays(1);

        InvoiceBuilder InvoiceBuilder = new InvoiceBuilder();
        Invoice newInvoice = InvoiceBuilder
            .Title(title)
            .Amount(agreedPrice)
            .OwnerId(owner)
            .Creation(creationDate)
            .Build();

        Assert.That(newInvoice.Title, Is.EqualTo(title));
        Assert.That(newInvoice.Amount, Is.EqualTo(agreedPrice));
        Assert.That(newInvoice.OwnerId, Is.EqualTo(owner));
        Assert.That(newInvoice.Creation, Is.EqualTo(creationDate));
    }
}