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
    public void QuotesShouldBelongToSomeoneAndHaveATitle()
    {
        string ownerName = "testUser";
        string title = "New Boiler Quote";

        Quote quote = new Quote(ownerName, title);

        Assert.That(quote.getOwnerName(), Is.EqualTo(ownerName));
        Assert.That(quote.getTitle(), Is.EqualTo(title));
    }

    [Test]
    public void QuotesShouldKeepTrackOfCreationDate()
    {
        string ownerName = "testUser";
        string title = "New Boiler Quote";

        Quote quote = new Quote(ownerName, title);

        Assert.That(DateTime.UtcNow.CompareTo(quote.getCreation()), Is.AtLeast(0));
    }

    [Test]
    public void QuotesShouldHaveAListOfExpenses()
    {
        string ownerName = "testUser";
        string title = "New Boiler Quote";
        Expense exampleExpense = new Expense("new expense", 0, 0);

        Quote quote = new Quote(ownerName, title);
        quote.AddExpense(exampleExpense);

        Assert.That(quote.getExpenses().Count(), Is.EqualTo(1));
    }

    [Test]
    public void QuotesShouldGiveEstimationsAndSaveThatInformation()
    {
        string ownerName = "testUser";
        string title = "New Boiler Quote";
        decimal expenseCost = 30.00m;
        int expenseQuantity = 6;
        Expense expense = new Expense("new expense", expenseCost, expenseQuantity);


        Quote quote = new Quote(ownerName, title);
        quote.AddExpense(expense);
        quote.EstimateCosts();

        Assert.That(quote.getQuotePrice(), Is.EqualTo(expenseCost * expenseQuantity));
    }

    [Test]
    public void QuotesShouldReturnJobsWhenAccepted()
    {
        string ownerName = "testUser";
        string title = "New Boiler Quote";
        DateTime startDate = DateTime.UtcNow.AddDays(5);
        DateTime endDate = DateTime.UtcNow.AddDays(5);
        decimal agreedPrice = 500.00m;

        Quote quote = new Quote(ownerName, title);
        Job result = quote.Accept(startDate, endDate, agreedPrice);

        Assert.That(result, Is.Not.Null);
    }
}
