using Sorted.Domain;

namespace Tests.Domain;

public class JobTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void JobsShouldBeBuildable()
    {
        string title = "test title";
        decimal agreedPrice = 30.00m;
        string owner = "testUser";
        DateTime creationDate = DateTime.UtcNow.AddDays(1);
        DateTime startDate = DateTime.UtcNow.AddDays(1);

        JobBuilder jobBuilder = new JobBuilder();
        Job newJob = jobBuilder
            .Title(title)
            .AgreedPrice(agreedPrice)
            .OwnerId(owner)
            .Creation(creationDate)
            .StartDate(startDate)
            .Build();

        Assert.That(newJob.Title, Is.EqualTo(title));
        Assert.That(newJob.AgreedPrice, Is.EqualTo(agreedPrice));
        Assert.That(newJob.OwnerId, Is.EqualTo(owner));
        Assert.That(newJob.Creation, Is.EqualTo(creationDate));
        Assert.That(newJob.StartDate, Is.EqualTo(startDate));
    }
}