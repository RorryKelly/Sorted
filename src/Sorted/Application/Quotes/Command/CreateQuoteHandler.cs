

using MediatR;
using Sorted.Domain;

namespace Sorted.Application.Commands;

public class CreateQuoteHandler : IRequestHandler<CreateQuoteCommand, string>
{
    private ICreateRepository<Quote, string> _repository;
    private IIdentityService _identityService;

    public CreateQuoteHandler(ICreateRepository<Quote, string> repository, IIdentityService identityService)
    {
        _repository = repository;
        _identityService = identityService;
    }

    public async Task<string> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        Result<string> userIdResult = await _identityService.GetUserId();

        if (!userIdResult.IsSuccess)
        {
            throw new Exception();
        }

        QuoteBuilder quoteBuilder = new QuoteBuilder();
        Quote newQuote = quoteBuilder
            .Title(request.title)
            .OwnerId(userIdResult.Value)
            .Creation(request.creation)
            .JobId(request.jobId)
            .Build();

        Result<string> result = await _repository.Create(newQuote, cancellationToken);

        return result.Value;
    }
}