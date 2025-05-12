
using Sorted.Domain.ValueObject;

namespace Sorted.Domain;

public class Job
{
    private string _title;
    private string _ownerName;
    private DateTime _startDate;
    private DateTime _endDate;
    private DateTime _creation;
    private List<Invoice> _invoices;
    private List<Expense> _expenses;
    private decimal _agreedPrice;

    public Job(string title, decimal agreedPrice, string owner, DateTime creationDate, DateTime startDate)
    {
        _title = title;
        _agreedPrice = agreedPrice;
        _ownerName = owner;
        _creation = creationDate;
        _startDate = startDate;
        _invoices = new List<Invoice>();
        _expenses = new List<Expense>();
    }

    public Job(string title, string ownerName, DateTime startDate, DateTime endDate, DateTime creation, List<Expense> expenses, decimal agreedPrice)
    {
        _title = title;
        _ownerName = ownerName;
        _startDate = startDate;
        _endDate = endDate;
        _creation = creation;
        _expenses = expenses;
        _agreedPrice = agreedPrice;
        _invoices = new List<Invoice>();
    }

    public void createInvoice(Invoice newInvoice)
    {
        if (_invoices == null)
        {
            _invoices = new List<Invoice>();
        }

        _invoices.Add(newInvoice);
    }

    public void createExpense(Expense newExpense)
    {
        if (_expenses == null)
        {
            _expenses = new List<Expense>();
        }

        _expenses.Add(newExpense);
    }

    public decimal getAgreedPrice()
    {
        return _agreedPrice;
    }

    public DateTime getCreationDate()
    {
        return _creation;
    }

    public string getOwner()
    {
        return _ownerName;
    }

    public DateTime getStartDate()
    {
        return _startDate;
    }

    public string getTitle()
    {
        return _title;
    }

    public List<Expense> getExpenses()
    {
        return _expenses;
    }

    public List<Invoice> getInvoices()
    {
        return _invoices;
    }

    public decimal getAmountPaid()
    {
        decimal amountPaid = 0;
        foreach (var invoice in _invoices)
        {
            if (invoice.hasPaid())
            {
                amountPaid += invoice.getAmount();
            }
        }

        return amountPaid;
    }
}