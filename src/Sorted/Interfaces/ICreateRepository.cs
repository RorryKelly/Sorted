public interface ICreateRepository<T1, T2>
{
    Task<Result<T2>> Create(T1 payload, CancellationToken cancellationToken);
}