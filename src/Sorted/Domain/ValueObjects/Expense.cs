
namespace Sorted.Domain.ValueObject;

public class Expense : IDomainAccess
{
    public string Title { get; set; }
    public string OwnerId { get; set; }
    public string JobId { get; set; }
    public DateTime Creation { get; set; }
    public decimal Cost { get; set; }
    public int Quantity { get; set; }
}

public class ExpenseBuilder
{
    private Expense _expense;

    public ExpenseBuilder()
    {
        _expense = new Expense();
    }

    public ExpenseBuilder Title(string title)
    {
        _expense.Title = title;
        return this;
    }

    public ExpenseBuilder OwnerId(string ownerId)
    {
        _expense.OwnerId = ownerId;
        return this;
    }

    public ExpenseBuilder JobId(string jobId)
    {
        _expense.JobId = jobId;
        return this;
    }

    public ExpenseBuilder Creation(DateTime creation)
    {
        _expense.Creation = creation;
        return this;
    }

    public ExpenseBuilder Cost(decimal cost)
    {
        _expense.Cost = cost;
        return this;
    }

    public ExpenseBuilder Quantity(int quantity)
    {
        _expense.Quantity = quantity;
        return this;
    }

    public Expense Build()
    {
        if (_expense.Creation == DateTime.MinValue)
        {
            _expense.Creation = DateTime.UtcNow;
        }
        return _expense;
    }
}