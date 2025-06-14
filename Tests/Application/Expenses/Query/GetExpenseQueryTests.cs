using Moq;
using Sorted.Application.Query;
using Sorted.Domain;
using Sorted.Domain.ValueObject;

namespace Tests.Application.Queries;

public class GetExpenseQueryTests
{
    GetExpenseHandler getExpenseHandler;

    Mock<IReadRepository<GetExpenseQuery, Expense>> readRepository;

    Mock<IIdentityService> identityService;

    [SetUp]
    public void SetUp()
    {
        identityService = new Mock<IIdentityService>();
        readRepository = new Mock<IReadRepository<GetExpenseQuery, Expense>>();
        getExpenseHandler = new GetExpenseHandler(readRepository.Object, identityService.Object);
    }

    [Test]
    public async Task ExpenseQueriesShouldReturnCorrectResultAsync()
    {
        string jobId = "job id";
        const string ownerId = "ownerName";
        const string title = "title";
        DateTime creation = DateTime.MinValue.AddDays(1);
        decimal cost = 3.0m;
        int quantity = 1;
        string userId = "userId";
        Expense expense = new ExpenseBuilder()
            .Title(title)
            .OwnerId(ownerId)
            .JobId(jobId)
            .Creation(creation)
            .Cost(cost)
            .Quantity(quantity)
            .Build();
        GetExpenseQuery getExpenseQuery = new GetExpenseQuery(title, ownerId, creation, jobId, cost, quantity);
        readRepository.Setup(r => r.FindAll(getExpenseQuery, CancellationToken.None))
            .ReturnsAsync(Result<List<Expense>>.Success(new List<Expense>() { expense }));
        Access userAccess = new Access() { Read = true, Write = true };
        identityService.Setup(id => id.GetAccess(expense, CancellationToken.None)).ReturnsAsync(Result<Access>.Success(userAccess));

        List<GetExpenseResponse> result = await getExpenseHandler.Handle(getExpenseQuery, CancellationToken.None);
        GetExpenseResponse getExpenseResponse = result.First();

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(getExpenseResponse.Title, Is.EqualTo(expense.Title));
        Assert.That(getExpenseResponse.OwnerId, Is.EqualTo(expense.OwnerId));
        Assert.That(getExpenseResponse.Creation, Is.EqualTo(expense.Creation));
    }
}