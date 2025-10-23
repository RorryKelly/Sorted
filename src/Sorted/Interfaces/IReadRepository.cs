public interface IReadRepository<T1, T2>
{
    Task<Result<List<T2>>> FindAll(T1 query, CancellationToken token);

    Task<Result<T2>> FindFirst(T1 query, CancellationToken token);
}