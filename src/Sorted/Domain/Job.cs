
using Sorted.Domain.ValueObject;

namespace Sorted.Domain;

public class Job : IDomainAccess
{
    public string Title;
    public string OwnerId;
    public DateTime StartDate;
    public DateTime EndDate;
    public DateTime Creation;
    public decimal AgreedPrice;
}

public class JobBuilder
{
    private Job _job;

    public JobBuilder()
    {
        _job = new Job();
    }

    public JobBuilder Title(string title)
    {
        _job.Title = title;
        return this;
    }

    public JobBuilder OwnerId(string ownerId)
    {
        _job.OwnerId = ownerId;
        return this;
    }

    public JobBuilder StartDate(DateTime startDate)
    {
        _job.StartDate = startDate;
        return this;
    }

    public JobBuilder EndDate(DateTime endDate)
    {
        _job.EndDate = endDate;
        return this;
    }

    public JobBuilder Creation(DateTime creation)
    {
        _job.Creation = creation;
        return this;
    }

    public JobBuilder AgreedPrice(decimal agreedPrice)
    {
        _job.AgreedPrice = agreedPrice;
        return this;
    }

    public Job Build()
    {
        if (_job.Creation == DateTime.MinValue)
        {
            _job.Creation = DateTime.UtcNow;
        }
        return _job;
    }
}