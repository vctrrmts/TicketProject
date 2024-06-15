using MediatR;
using TicketEventSearch.Application.Abstractions;

namespace TicketEventSearch.Application.BaseRealizations;

public abstract class BaseCashedQuery<TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    private readonly IBaseCache<TResult> _cache;

    public BaseCashedQuery(IBaseCache<TResult> cache)
    {
        _cache = cache;
    }

    public async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
    {
        if (_cache.TryGetValue(request, out TResult? result))
        {
            return result!;
        }

        result = await SentQueryAsync(request, cancellationToken);

        _cache.Set(request, result, 1);
        return result;
    }

    public abstract Task<TResult> SentQueryAsync(TRequest request, CancellationToken cancellationToken);
}