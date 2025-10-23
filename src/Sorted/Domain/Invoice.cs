
namespace Sorted.Domain;

public class Invoice : IDomainAccess
{
    public string Id { get; set; }
    public string Title { get; set; }
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; }
    public string OwnerId { get; set; }
    public string JobId { get; set; }
    public DateTime Creation { get; set; }
    public DateTime DatePaid { get; set; }
}

public class InvoiceBuilder
{
    private Invoice _invoice;

    public InvoiceBuilder()
    {
        _invoice = new Invoice();
    }

    public InvoiceBuilder Title(string title)
    {
        _invoice.Title = title;
        return this;
    }

    public InvoiceBuilder OwnerId(string ownerId)
    {
        _invoice.OwnerId = ownerId;
        return this;
    }

    public InvoiceBuilder Creation(DateTime creation)
    {
        _invoice.Creation = creation;
        return this;
    }

    public InvoiceBuilder JobId(string jobId)
    {
        _invoice.JobId = jobId;
        return this;
    }

    public InvoiceBuilder Amount(decimal amount)
    {
        _invoice.Amount = amount;
        return this;
    }

    public InvoiceBuilder DatePaid(DateTime datePaid)
    {
        _invoice.DatePaid = datePaid;
        _invoice.IsPaid = true;
        return this;
    }

    public Invoice Build()
    {
        if (_invoice.Creation == DateTime.MinValue)
        {
            _invoice.Creation = DateTime.UtcNow;
        }
        return _invoice;
    }
}