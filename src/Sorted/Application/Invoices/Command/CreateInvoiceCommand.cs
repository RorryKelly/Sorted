using MediatR;

public record CreateInvoiceCommand(string title, string ownerId, string jobId, DateTime startDate, DateTime DatePaid, DateTime creation, decimal amount) : IRequest<string>;