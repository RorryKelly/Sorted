
using Sorted.Domain.ValueObject;

namespace Sorted.Domain;

public class Job : IDomainAccess
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string OwnerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime Creation { get; set; }
    public decimal AgreedPrice { get; set; }
}

public class JobBuilder
{
    private Job _job;

    public JobBuilder()
    {
        _job = new Job();
    }

    public JobBuilder Id(string id)
    {
        _job.Id = id;
        return this;
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