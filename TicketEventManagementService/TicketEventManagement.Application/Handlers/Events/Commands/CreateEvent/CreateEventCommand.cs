using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Events.Commands.CreateEvent
{
    public class CreateEventCommand : IRequest<GetEventDto>
    {
        public Guid LocationId { get; set; }
        public Guid CategoryId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string UriMainImage { get; set; } = default!;
        public DateTime DateTimeEventStart { get; set; }
        public DateTime DateTimeEventEnd { get; set; }

    }
}
