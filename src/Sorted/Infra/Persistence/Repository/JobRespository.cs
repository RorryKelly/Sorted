using Sorted.Domain;

public class JobRepository : ICreateRepository<Job, string>, IReadRepository<GetJobQuery, Job>
{
    public Task<Result<string>> Create(Job payload, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Job>>> FindAll(GetJobQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Job>> FindFirst(GetJobQuery query, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}