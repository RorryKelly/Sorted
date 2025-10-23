using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sorted.Domain;
using Sorted.Infra.Persistence;

public class InvoiceRepository : ICreateRepository<Invoice, string>, IReadRepository<GetInvoiceQuery, Invoice>
{
    private ApplicationSqlDbContext _dbContext;

    public InvoiceRepository(ApplicationSqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<string>> Create(Invoice payload, CancellationToken cancellationToken)
    {
        EntityEntry<Invoice> entityEntry = await _dbContext.Invoices.AddAsync(payload, cancellationToken);
        Result<string> result;
        switch (entityEntry.State)
        {
            case EntityState.Added:
                result = Result<string>.Success(entityEntry.Entity.Id);
                _dbContext.SaveChanges();
                break;
            case EntityState.Unchanged:
                result = Result<string>.Failure(["Job already exists!"]);
                break;
            default:
                result = Result<string>.Failure(["Couldn't add job. Unexpected error occured"]);
                break;
        }

        return result;
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