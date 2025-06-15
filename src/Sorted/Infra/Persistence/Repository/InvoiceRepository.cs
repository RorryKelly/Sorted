using Sorted.Domain;

public class InvoiceRepository : ICreateRepository<Invoice, string>, IReadRepository<GetInvoiceQuery, Invoice>
{
    public Task<Result<string>> Create(Invoice payload, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Invoice>>> FindAll(GetInvoiceQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Invoice>> FindFirst(GetInvoiceQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}