using MediatR;
using Sorted.Application.Common;
using Sorted.Domain;

namespace Sorted.Application.Query;

public class GetInvoiceHandler : IQueryHandler<GetInvoiceQuery, List<GetInvoiceResponse>>
{
    private IReadRepository<GetInvoiceQuery, Invoice> _readRepository;
    private IIdentityService _identityService;

    public GetInvoiceHandler(IReadRepository<GetInvoiceQuery, Invoice> readRepository, IIdentityService identityService)
    {
        _readRepository = readRepository;
        _identityService = identityService;
    }

    public async Task<List<GetInvoiceResponse>> Handle(GetInvoiceQuery request, CancellationToken cancellationToken)
    {
        Result<List<Invoice>> result = await _readRepository.FindAll(request, cancellationToken);

        List<Invoice> invoiceList = result.Value;
        List<GetInvoiceResponse> invoiceDto = new List<GetInvoiceResponse>();

        foreach (Invoice invoice in invoiceList)
        {
            Result<Access> access = await _identityService.GetAccess(invoice, cancellationToken);
            if (access.IsSuccess && access.Value.Read)
            {
                invoiceDto.Add(invoice.MapToInvoiceResponse());
            }
        }

        return invoiceDto;
    }
}
