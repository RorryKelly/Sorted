using Moq;
using Sorted.Application.Query;
using Sorted.Domain;

namespace Tests.Application.Queries;

public class GetJobQueryTests
{
    GetJobHandler getJobHandler;

    Mock<IReadRepository<GetJobQuery, Job>> readRepository;

    Mock<IIdentityService> identityService;

    [SetUp]
    public void SetUp()
    {
        identityService = new Mock<IIdentityService>();
        readRepository = new Mock<IReadRepository<GetJobQuery, Job>>();
        getJobHandler = new GetJobHandler(readRepository.Object, identityService.Object);
    }

    [Test]
    public async Task JobQueriesShouldReturnCorrectResultAsync()
    {
        string title = "test title";
        decimal agreedPrice = 30.00m;
        string owner = "testUser";
        DateTime creationDate = DateTime.UtcNow.AddDays(1);
        DateTime startDate = DateTime.UtcNow.AddDays(1);
        Job job = new JobBuilder()
            .Title(title)
            .AgreedPrice(agreedPrice)
            .OwnerId(owner)
            .Creation(creationDate)
            .StartDate(startDate)
            .Build();
        GetJobQuery getJobQuery = new GetJobQuery("jobId", owner, title);
        readRepository.Setup(r => r.FindAll(getJobQuery, CancellationToken.None))
            .ReturnsAsync(Result<List<Job>>.Success(new List<Job>() { job }));
        Access userAccess = new Access() { Read = true, Write = true };
        identityService.Setup(id => id.GetAccess(job, CancellationToken.None)).ReturnsAsync(Result<Access>.Success(userAccess));

        List<GetJobResponse> result = await getJobHandler.Handle(getJobQuery, CancellationToken.None);
        GetJobResponse getJobResponse = result.First();

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(getJobResponse.Title, Is.EqualTo(job.Title));
        Assert.That(getJobResponse.OwnerName, Is.EqualTo(job.OwnerId));
        Assert.That(getJobResponse.AgreedPrice, Is.EqualTo(job.AgreedPrice));
    }
}