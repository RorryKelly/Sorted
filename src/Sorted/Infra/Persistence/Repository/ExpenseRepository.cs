using Sorted.Domain.ValueObject;

public class ExpenseRepository : ICreateRepository<Expense, string>, IReadRepository<GetExpenseQuery, Expense>
{
    public Task<Result<string>> Create(Expense payload, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Expense>>> FindAll(GetExpenseQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Expense>> FindFirst(GetExpenseQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}