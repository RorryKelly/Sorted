
namespace Sorted.Domain.ValueObject;

public class Expense
{
    public string _title { get; set; }
    public decimal ExpenseCost { get; set; }
    public int ExpenseQuantity { get; set; }

    public Expense(string title, decimal expenseCost, int expenseQuantity)
    {
        _title = title;
        ExpenseCost = expenseCost;
        ExpenseQuantity = expenseQuantity;
    }
}