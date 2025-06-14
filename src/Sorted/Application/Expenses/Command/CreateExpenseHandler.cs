

using MediatR;
using Sorted.Domain;
using Sorted.Domain.ValueObject;

namespace Sorted.Application.Commands;

public class CreateExpenseHandler : IRequestHandler<CreateExpenseCommand, string>
{
    private ICreateRepository<Expense, string> _repository;
    private IIdentityService _identityService;

    public CreateExpenseHandler(ICreateRepository<Expense, string> repository, IIdentityService identityService)
    {
        _repository = repository;
        _identityService = identityService;
    }

    public async Task<string> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        Result<string> userIdResult = _identityService.GetUserId();

        if (!userIdResult.IsSuccess)
        {
            throw new Exception();
        }

        ExpenseBuilder expenseBuilder = new ExpenseBuilder();
        Expense newExpense = expenseBuilder
            .Title(request.title)
            .Creation(request.creation)
            .JobId(request.jobId)
            .Cost(request.cost)
            .Quantity(request.quantity)
            .Build();

        Result<string> result = await _repository.Create(newExpense, cancellationToken);

        return result.Value;
    }
}