using MediatR;
using Notification.Application.Abstractions.Persistence;
using Notification.Domain;

namespace Notification.Application.Handlers.Commands.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
{
    private readonly IBaseRepository<Event> _events;

    public CreateEventCommandHandler(IBaseRepository<Event> events) 
    {
        _events = events;
    }

    public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var myEvent = new Event(request.EventId);
        await _events.AddAsync(myEvent, cancellationToken);
    }
}
