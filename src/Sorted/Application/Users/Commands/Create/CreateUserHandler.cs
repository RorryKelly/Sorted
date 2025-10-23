

using MediatR;
using Sorted.Domain;

namespace Sorted.Application.Commands;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, string>
{
    private IIdentityService _identityService;

    public CreateUserHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = new UserBuilder()
            .Username(request.username)
            .Email(request.emailAddress)
            .Build();
        Result<string> result = await _identityService.CreateUser(user, request.password);

        return result.Value;
    }
}