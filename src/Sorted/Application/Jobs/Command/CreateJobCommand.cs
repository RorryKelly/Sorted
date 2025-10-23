using MediatR;

public record CreateJobCommand(string title, string ownerName, DateTime startDate, DateTime EndDate, DateTime creation, decimal agreedPrice) : IRequest<string>;