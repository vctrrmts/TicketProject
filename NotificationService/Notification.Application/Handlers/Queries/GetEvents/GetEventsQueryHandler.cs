using MediatR;
using Notification.Domain;
using Notification.Application.Abstractions.Persistence;

namespace Notification.Application.Handlers.Queries.GetEvents;

public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, IReadOnlyCollection<Event>>
{
    private readonly IBaseRepository<Event> _events;

    public GetEventsQueryHandler(IBaseRepository<Event> events)
    {
        _events = events;
    }

    public async Task<IReadOnlyCollection<Event>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        return await _events.GetListAsync(
            default,
            default,
            default,
            default,
            default,
            cancellationToken);
    }
}
