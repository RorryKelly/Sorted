using Microsoft.AspNetCore.Builder;
using Moq;
using Sorted.Application.Commands;
using Sorted.Domain;

namespace Tests.Application.Commands;

public class JobTests
{
    CreateJobHandler createJobHandler;
    Mock<ICreateRepository<Job, string>> repository;
    Mock<IIdentityService> identityService;

    [SetUp]
    public void Setup()
    {
        identityService = new Mock<IIdentityService>();
        repository = new Mock<ICreateRepository<Job, string>>();
        createJobHandler = new CreateJobHandler(repository.Object, identityService.Object);
    }

    [Test]
    public async Task JobsCreationShouldCallPersistenceLayerAsync()
    {
        const string ownerName = "ownerName";
        const string title = "title";
        DateTime startDate = DateTime.MinValue.AddDays(1);
        DateTime endDate = DateTime.MinValue.AddDays(2);
        DateTime creation = DateTime.MinValue.AddDays(1);
        decimal agreedPrice = 3.0m;
        string userId = "userId";
        repository.Setup(r => r.Create(It.IsAny<Job>(), CancellationToken.None)).ReturnsAsync(Result<string>.Success("Success"));
        identityService.Setup(id => id.GetUserId()).Returns(Result<string>.Success(userId));
        CreateJobCommand createJobHandlerCommand = new CreateJobCommand(title, ownerName, startDate, endDate, creation, agreedPrice);

        await createJobHandler.Handle(createJobHandlerCommand, CancellationToken.None);

        repository.Verify(r => r.Create(It.IsAny<Job>(), CancellationToken.None));
    }
}