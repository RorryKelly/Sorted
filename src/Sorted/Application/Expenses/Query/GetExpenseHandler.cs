using MediatR;
using Sorted.Application.Common;
using Sorted.Domain;
using Sorted.Domain.ValueObject;

namespace Sorted.Application.Query;

public class GetExpenseHandler : IQueryHandler<GetExpenseQuery, List<GetExpenseResponse>>
{
    private IReadRepository<GetExpenseQuery, Expense> _readRepository;
    private IIdentityService _identityService;

    public GetExpenseHandler(IReadRepository<GetExpenseQuery, Expense> readRepository, IIdentityService identityService)
    {
        _readRepository = readRepository;
        _identityService = identityService;
    }

    public async Task<List<GetExpenseResponse>> Handle(GetExpenseQuery request, CancellationToken cancellationToken)
    {
        Result<List<Expense>> result = await _readRepository.FindAll(request, cancellationToken);

        List<Expense> expenseList = result.Value;
        List<GetExpenseResponse> expenseDto = new List<GetExpenseResponse>();

        foreach (Expense expense in expenseList)
        {
            Result<Access> access = await _identityService.GetAccess(expense, cancellationToken);
            if (access.IsSuccess && access.Value.Read)
            {
                expenseDto.Add(expense.MapToExpenseResponse());
            }
        }

        return expenseDto;
    }
}
