using MediatR;

public record CreateUserCommand(string username, string password, string emailAddress) : IRequest<string>;