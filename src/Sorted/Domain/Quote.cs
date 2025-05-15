

using Sorted.Application.Jobs;
using Sorted.Domain.ValueObject;

namespace Sorted.Domain;

public class Quote
{
    private Guid _id;
    private Guid _userId;
    private Guid _parentId;
    private string _title;

    private DateTime _creation;

    private decimal _askedPrice;

    public Quote()
    {
        _creation = DateTime.UtcNow;
    }

    public Quote(CreateQuoteCommand request)
    {
        _parentId = new Guid(request.parentId);
        _title = request.title;
        _creation = DateTime.UtcNow;
    }


    public void setUserId(Guid userId)
    {
        _userId = userId;
    }

    public Guid getUserId()
    {
        return _userId;
    }

    public string getTitle()
    {
        return _title;
    }

    public DateTime getCreation()
    {
        return _creation;
    }

    public decimal getQuotePrice()
    {
        return _askedPrice;
    }

    public Guid getParentId()
    {
        return _parentId;
    }
}