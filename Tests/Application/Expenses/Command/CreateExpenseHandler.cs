using Moq;
using Sorted.Application.Commands;
using Sorted.Domain;
using Sorted.Domain.ValueObject;

namespace Tests.Application.Commands;

public class ExpenseTests
{
    CreateExpenseHandler createExpenseHandler;
    Mock<ICreateRepository<Expense, string>> repository;
    Mock<IIdentityService> identityService;

    [SetUp]
    public void Setup()
    {
        identityService = new Mock<IIdentityService>();
        repository = new Mock<ICreateRepository<Expense, string>>();
        createExpenseHandler = new CreateExpenseHandler(repository.Object, identityService.Object);
    }

    [Test]
    public async Task ExpensesCreationShouldCallPersistenceLayerAsync()
    {
        string jobId = "job id";
        const string ownerId = "ownerName";
        const string title = "title";
        DateTime creation = DateTime.MinValue.AddDays(1);
        decimal cost = 3.0m;
        int quantity = 1;
        string userId = "userId";
        repository.Setup(r => r.Create(It.IsAny<Expense>(), CancellationToken.None)).ReturnsAsync(Result<string>.Success("Success"));
        identityService.Setup(id => id.GetUserId()).Returns(Result<string>.Success(userId));
        CreateExpenseCommand createExpenseHandlerCommand = new CreateExpenseCommand(title, ownerId, creation, jobId, cost, quantity);

        await createExpenseHandler.Handle(createExpenseHandlerCommand, CancellationToken.None);

        repository.Verify(r => r.Create(It.IsAny<Expense>(), CancellationToken.None));
    }
}