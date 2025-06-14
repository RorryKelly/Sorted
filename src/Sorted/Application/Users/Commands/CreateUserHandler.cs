

using MediatR;
using Microsoft.Identity.Client;
using Sorted.Domain;

namespace Sorted.Application.Commands;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, string>
{
    private ICreateRepository<User, string> _repository;
    private IIdentityService _identityService;

    public CreateUserHandler(ICreateRepository<User, string> repository, IIdentityService identityService)
    {
        _repository = repository;
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