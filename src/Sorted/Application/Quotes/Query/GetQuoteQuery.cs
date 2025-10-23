using MediatR;
using Sorted.Domain;

public record GetQuoteQuery(string jobId, string ownerId, string name) : IQuery<List<GetQuoteResponse>>;