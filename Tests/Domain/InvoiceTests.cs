using Sorted.Domain;

namespace Tests.Domain;

public class InvoiceTests
{
    [Test]
    public void InvoicesShouldBeAbleToBeSetToPaid()
    {
        string invoiceTitle = "New Invoice";
        decimal invoiceAmount = 50;
        bool isPaid = false;
        DateTime creationDate = DateTime.Now;

        Invoice invoice = new Invoice(invoiceTitle, invoiceAmount, isPaid, creationDate);
        invoice.Pay();

        Assert.That(invoice.hasPaid(), Is.EqualTo(true));
    }

}