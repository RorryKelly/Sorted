using Sorted.Domain;
using Sorted.Domain.ValueObject;

namespace Tests.Domain;

public class JobTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void JobsShouldHaveATitleAndAgreedPriceAndCreationDateAndOwnerAndStartDate()
    {
        string title = "test title";
        decimal agreedPrice = 30.00m;
        string owner = "testUser";
        DateTime creationDate = DateTime.UtcNow.AddDays(1);
        DateTime startDate = DateTime.UtcNow.AddDays(1);

        Job newJob = new Job(title, agreedPrice, owner, creationDate, startDate);

        Assert.That(newJob.getTitle(), Is.EqualTo(title));
        Assert.That(newJob.getAgreedPrice(), Is.EqualTo(agreedPrice));
        Assert.That(newJob.getOwner(), Is.EqualTo(owner));
        Assert.That(newJob.getCreationDate(), Is.EqualTo(creationDate));
        Assert.That(newJob.getStartDate(), Is.EqualTo(startDate));
    }

    [Test]
    public void JobsShouldBeAbleToGenerateInvoicesAndExpenses()
    {
        string title = "test title";
        decimal agreedPrice = 30.00m;
        string owner = "testUser";
        DateTime creationDate = DateTime.UtcNow.AddDays(1);
        DateTime startDate = DateTime.UtcNow.AddDays(1);
        string invoiceTitle = "New invoice";
        decimal invoiceAmount = 10.00m;
        bool isPaid = false;
        Invoice newInvoice = new Invoice(invoiceTitle, invoiceAmount, isPaid, creationDate);
        string expenseTitle = "New Expense";
        decimal expenseCost = 10.00m;
        int expenseQuantity = 1;
        Expense newExpense = new Expense(expenseTitle, expenseCost, expenseQuantity);

        Job newJob = new Job(title, agreedPrice, owner, creationDate, startDate);
        newJob.createInvoice(newInvoice);
        newJob.createExpense(newExpense);

        Assert.That(newJob.getInvoices().Count(), Is.EqualTo(1));
        Assert.That(newJob.getExpenses().Count(), Is.EqualTo(1));
    }

    [Test]
    public void JobsShouldBeAbleToTrackHowMuchTheyHaveBeenPaidForTheJob()
    {
        string title = "test title";
        decimal agreedPrice = 30.00m;
        string owner = "testUser";
        DateTime creationDate = DateTime.UtcNow.AddDays(1);
        DateTime startDate = DateTime.UtcNow.AddDays(1);
        string invoiceTitle = "New invoice";
        decimal expectedAmount = 10.00m;
        bool isPaid = true;
        Invoice newInvoice = new Invoice(invoiceTitle, expectedAmount, isPaid, creationDate);

        Job newJob = new Job(title, agreedPrice, owner, creationDate, startDate);
        newJob.createInvoice(newInvoice);
        decimal actualAmount = newJob.getAmountPaid();

        Assert.That(actualAmount, Is.EqualTo(expectedAmount));
    }
}