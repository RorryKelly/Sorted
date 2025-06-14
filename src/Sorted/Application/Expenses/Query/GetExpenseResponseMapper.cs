using Sorted.Domain.ValueObject;

public static class GetExpenseResponseMapper
{
    public static GetExpenseResponse MapToExpenseResponse(this Expense expense)
    {
        return new GetExpenseResponse()
        {
            Title = expense.Title,
            OwnerId = expense.OwnerId,
            JobId = expense.JobId,
            Creation = expense.Creation,
            Cost = expense.Cost,
            Quantity = expense.Quantity
        };
    }
}