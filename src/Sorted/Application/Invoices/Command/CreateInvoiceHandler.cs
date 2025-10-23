

using MediatR;
using Sorted.Domain;

namespace Sorted.Application.Commands;

public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand, string>
{
    private ICreateRepository<Invoice, string> _repository;
    private IIdentityService _identityService;

    public CreateInvoiceHandler(ICreateRepository<Invoice, string> repository, IIdentityService identityService)
    {
        _repository = repository;
        _identityService = identityService;
    }

    public async Task<string> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
    {
        Result<string> userIdResult = await _identityService.GetUserId();

        InvoiceBuilder invoiceBuilder = new InvoiceBuilder();
        Invoice newInvoice = invoiceBuilder
            .Title(request.title)
            .OwnerId(userIdResult.Value)
            .Amount(request.amount)
            .Creation(request.creation)
            .JobId(request.jobId)
            .Build();

        Result<string> result = await _repository.Create(newInvoice, cancellationToken);

        return result.Value;
    }
}