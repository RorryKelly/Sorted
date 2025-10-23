using MediatR;

public record LoginCommand(string username, string email, string password) : IRequest<string>;