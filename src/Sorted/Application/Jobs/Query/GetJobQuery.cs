using MediatR;
using Sorted.Domain;

public record GetJobQuery(string jobId, string ownerId, string name) : IQuery<List<GetJobResponse>>;