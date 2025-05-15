using Moq;
using Sorted.Application.Jobs;

public class CreateJobTests()
{
    CreateJobHandler createJobHandler;
    Mock<IJobRepository> jobRepo;
    Mock<IIdentityService> identityService;

    [SetUp]
    public void SetUp()
    {
        jobRepo = new Mock<IJobRepository>();
        identityService = new Mock<IIdentityService>();
        createJobHandler = new CreateJobHandler(jobRepo.Object, identityService.Object);
    }

    [Test]
    public void UsersShouldBeAbleToSaveJobsToPersistenceLayer()
    {
        CancellationToken token = new CancellationToken();
        CreateQuoteCommand command = new CreateQuoteCommand(new Guid().ToString(), "Title");
        identityService.Setup(id => id.GetUserId()).Returns(new Guid());
        identityService.Setup(id => id.HasAccess(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        // await createJobHandler.Handle(command, token);

        // quoteRepository.Verify(repo => repo.SaveQuote(It.IsAny<Job>(), token));
    }
}