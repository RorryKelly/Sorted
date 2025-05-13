

using Sorted.Domain.ValueObject;

namespace Sorted.Domain;

public class Quote
{
    private string _ownerName { get; }
    private string _title { get; }

    private DateTime _creation;

    private List<Expense> _expenses;

    private decimal _askedPrice;

    public Quote(string ownerName, string title)
    {
        _ownerName = ownerName;
        _title = title;
        _creation = DateTime.UtcNow;
    }

    public Quote(string ownerName, string title, decimal askedPrice)
    {
        _ownerName = ownerName;
        _title = title;
        _creation = DateTime.UtcNow;
        _askedPrice = askedPrice;
    }

    public string getOwnerName()
    {
        return _ownerName;
    }

    public string getTitle()
    {
        return _title;
    }

    public DateTime getCreation()
    {
        return _creation;
    }

    public void AddExpense(Expense exampleExpense)
    {
        if (_expenses == null)
        {
            _expenses = new List<Expense>();
        }
        _expenses.Add(exampleExpense);
    }

    public List<Expense> getExpenses()
    {
        return _expenses;
    }

    public decimal getQuotePrice()
    {
        return _askedPrice;
    }

    public void EstimateCosts()
    {
        decimal estimatedCost = 0.00m;
        foreach (var expense in _expenses)
        {
            estimatedCost += expense.ExpenseCost * expense.ExpenseQuantity;
        }

        _askedPrice = estimatedCost;
    }

    public Job Accept(DateTime startDate, DateTime endDate, decimal agreedPrice)
    {
        return new Job(_title, _ownerName, startDate, endDate, _creation, _expenses, agreedPrice);
    }
}