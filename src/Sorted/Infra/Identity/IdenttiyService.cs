
using Microsoft.AspNetCore.Identity;

public class IdentityService : IIdentityService
{
    private UserManager<User> _userManager;

    public IdentityService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<string>> CreateUser(User request, string password)
    {
        await _userManager.CreateAsync(request, password);

        return Result<string>.Success("");
    }

    public Task<Result<Access>> GetAccess(IDomainAccess item, CancellationToken none)
    {
        throw new NotImplementedException();
    }

    public Result<string> GetUserId()
    {
        throw new NotImplementedException();
    }
}