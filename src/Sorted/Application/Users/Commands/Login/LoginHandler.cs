

using MediatR;
using Sorted.Domain;

namespace Sorted.Application.Commands;

public class LoginHandler : IRequestHandler<LoginCommand, string>
{
    private IIdentityService _identityService;

    public LoginHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        Result<string> result = await _identityService.SignIn(request.email, request.username, request.password);

        return result.Value;
    }
}