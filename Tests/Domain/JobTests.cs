using Sorted.Domain;
using Sorted.Domain.ValueObject;

namespace Tests.Domain;

public class JobTests
{
    [Test]
    public void JobsShouldHaveATitleAndAgreedPriceAndCreationDateAndOwnerAndStartDate()
    {
        string title = "test title";
        decimal agreedPrice = 30.00m;
        Guid owner = Guid.NewGuid();
        DateTime creationDate = DateTime.UtcNow.AddDays(1);
        DateTime startDate = DateTime.UtcNow.AddDays(1);

        Job newJob = new Job(title, agreedPrice, owner, creationDate, startDate);

        Assert.That(newJob.getTitle(), Is.EqualTo(title));
        Assert.That(newJob.getAgreedPrice(), Is.EqualTo(agreedPrice));
        Assert.That(newJob.getOwner(), Is.EqualTo(owner));
        Assert.That(newJob.getCreationDate(), Is.EqualTo(creationDate));
        Assert.That(newJob.getStartDate(), Is.EqualTo(startDate));
    }
}