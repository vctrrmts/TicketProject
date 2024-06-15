using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Events.Commands.UpdateEvent;

public class UpdateEventCommand : IRequest<GetEventDto>
{
    public Guid EventId { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string UriMainImage { get; set; } = default!;
    public DateTime DateTimeEventStart { get; set; }
    public DateTime DateTimeEventEnd { get; set; }
    public bool IsActive { get; set; }
}
