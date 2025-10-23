using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sorted.Domain;
using Sorted.Infra.Persistence;

public class JobRepository : ICreateRepository<Job, string>, IReadRepository<GetJobQuery, Job>
{
    private ApplicationSqlDbContext _dbContext;

    public JobRepository(ApplicationSqlDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<string>> Create(Job payload, CancellationToken cancellationToken)
    {
        EntityEntry<Job> entityEntry = await _dbContext.Jobs.AddAsync(payload, cancellationToken);
        Result<string> result;
        switch (entityEntry.State)
        {
            case EntityState.Added:
                result = Result<string>.Success(entityEntry.Entity.Id);
                _dbContext.SaveChanges();
                break;
            case EntityState.Unchanged:
                result = Result<string>.Failure(["Job already exists!"]);
                break;
            default:
                result = Result<string>.Failure(["Couldn't add job. Unexpected error occured"]);
                break;
        }

        return result;
    }

    public async Task<Result<List<Job>>> FindAll(GetJobQuery query, CancellationToken token)
    {
        IQueryable<Job> jobsQueryable = Query(query, token);

        if (jobsQueryable.Count() == 0)
        {
            return Result<List<Job>>.Failure(["Cannot find any jobs"]);
        }

        List<Job> jobs = jobsQueryable.ToList();
        return Result<List<Job>>.Success(jobs);
    }

    public async Task<Result<Job>> FindFirst(GetJobQuery query, CancellationToken token)
    {
        IQueryable<Job> jobsQueryable = Query(query, token);

        if (jobsQueryable.Count() == 0)
        {
            return Result<Job>.Failure(["Cannot find any jobs"]);
        }

        Job job = jobsQueryable.First();
        return Result<Job>.Success(job);
    }

    private IQueryable<Job> Query(GetJobQuery query, CancellationToken token)
    {
        return _dbContext.Jobs.Where
        (
            job =>
                job.Id == query.Id
                && job.Title == query.Title
                && job.OwnerId == query.OwnerId
        );
    }
}