using MediatR;

namespace Sorted.Application.Common;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, TResult>
    where TQuery : IRequest<TResult>
{

}
