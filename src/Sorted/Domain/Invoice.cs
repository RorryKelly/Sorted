
namespace Sorted.Domain;

public class Invoice
{
    private string _parentId;
    private string _invoiceTitle;
    private decimal _invoiceAmount;
    private bool _isPaid;
    private DateTime _creationDate;
    private DateTime _datePaid;

    public Invoice(string invoiceTitle, decimal invoiceAmount, bool isPaid, DateTime creationDate)
    {
        _invoiceTitle = invoiceTitle;
        _invoiceAmount = invoiceAmount;
        _isPaid = isPaid;
        _creationDate = creationDate;
    }

    internal decimal getAmount()
    {
        return _invoiceAmount;
    }

    public void Pay()
    {
        _isPaid = true;
        _datePaid = DateTime.Now;
    }

    public bool hasPaid()
    {
        return _isPaid;
    }
}