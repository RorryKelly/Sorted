using MediatR;
using Sorted.Domain;

public record GetJobQuery(string Id, string OwnerId, string Title) : IQuery<List<GetJobResponse>>;