using MediatR;
using Sorted.Domain;

namespace Sorted.Application.Jobs;

public record CreateQuoteCommand(string parentId, string title) : IRequest<Guid>;
public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, Guid>
{
    private IQuoteRepository _repository;
    private IIdentityService _identityService;

    public CreateQuoteCommandHandler(IQuoteRepository repository, IIdentityService identityService)
    {
        _repository = repository;
        _identityService = identityService;
    }

    public async Task<Guid> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        Guid userId;
        userId = _identityService.GetUserId();
        if (!_identityService.HasAccess(request.parentId, userId.ToString()))
        {
            throw new UnauthorizedAccessException("User does not have access to create this quote");
        }

        Quote newQuote = new Quote(request);
        newQuote.setUserId(userId);

        Guid id = await _repository.SaveQuote(newQuote, cancellationToken);

        return id;
    }
}