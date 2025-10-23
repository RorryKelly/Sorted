using Sorted.Domain;

public interface IIdentityService
{
    Task<Result<string>> GetUserId();
    Task<Result<Access>> GetAccess(IDomainAccess item, CancellationToken none);
    Task<Result<string>> CreateUser(User request, string password);
    Task<Result<string>> SignIn(string email, string username, string password);
}