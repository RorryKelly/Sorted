using MediatR;

public record CreateQuoteCommand(string title, string ownerId, DateTime creation, string jobId, decimal agreedPrice) : IRequest<string>;