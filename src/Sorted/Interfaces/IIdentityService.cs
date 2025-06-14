using Sorted.Domain;

public interface IIdentityService
{
    Result<string> GetUserId();
    Task<Result<Access>> GetAccess(IDomainAccess item, CancellationToken none);
    Task<Result<string>> CreateUser(User request, string password);
}