using MediatR;
using Sorted.Application.Common;
using Sorted.Domain;

namespace Sorted.Application.Query;

public class GetQuoteHandler : IQueryHandler<GetQuoteQuery, List<GetQuoteResponse>>
{
    private IReadRepository<GetQuoteQuery, Quote> _readRepository;
    private IIdentityService _identityService;

    public GetQuoteHandler(IReadRepository<GetQuoteQuery, Quote> readRepository, IIdentityService identityService)
    {
        _readRepository = readRepository;
        _identityService = identityService;
    }

    public async Task<List<GetQuoteResponse>> Handle(GetQuoteQuery request, CancellationToken cancellationToken)
    {
        Result<List<Quote>> result = await _readRepository.FindAll(request, cancellationToken);

        List<Quote> quoteList = result.Value;
        List<GetQuoteResponse> quoteDto = new List<GetQuoteResponse>();

        foreach (Quote quote in quoteList)
        {
            Result<Access> access = await _identityService.GetAccess(quote, cancellationToken);
            if (access.IsSuccess && access.Value.Read)
            {
                quoteDto.Add(quote.MapToQuoteResponse());
            }
        }

        return quoteDto;
    }
}
