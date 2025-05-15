using Sorted.Domain;

public interface IJobRepository
{
    Task<Guid> SaveJob(Job job, CancellationToken token);
}