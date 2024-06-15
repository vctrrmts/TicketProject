using MediatR;
using Notification.Domain;

namespace Notification.Application.Handlers.Queries.GetEvents;

public class GetEventsQuery : IRequest<IReadOnlyCollection<Event>>
{
}
