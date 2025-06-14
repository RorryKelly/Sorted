using MediatR;

namespace Sorted.Application.Commands;

public record CreateExpenseCommand(string title, string ownerId, DateTime creation, string jobId, decimal cost, int quantity) : IRequest<string>;