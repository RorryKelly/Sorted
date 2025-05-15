
using Sorted.Domain.ValueObject;

namespace Sorted.Domain;

public class Job
{
    private string _title;
    private Guid _ownerUserId;
    private DateTime _startDate;
    private DateTime _endDate;
    private DateTime _creation;
    private decimal _agreedPrice;

    public Job(string title, decimal agreedPrice, Guid ownerUserId, DateTime creationDate, DateTime startDate)
    {
        _title = title;
        _agreedPrice = agreedPrice;
        _ownerUserId = ownerUserId;
        _creation = creationDate;
        _startDate = startDate;
    }

    public Job(string title, Guid ownerUserId, DateTime startDate, DateTime endDate, DateTime creation, decimal agreedPrice)
    {
        _title = title;
        _ownerUserId = ownerUserId;
        _startDate = startDate;
        _endDate = endDate;
        _creation = creation;
        _agreedPrice = agreedPrice;
    }

    public decimal getAgreedPrice()
    {
        return _agreedPrice;
    }

    public DateTime getCreationDate()
    {
        return _creation;
    }

    public Guid getOwner()
    {
        return _ownerUserId;
    }

    public DateTime getStartDate()
    {
        return _startDate;
    }

    public string getTitle()
    {
        return _title;
    }
}