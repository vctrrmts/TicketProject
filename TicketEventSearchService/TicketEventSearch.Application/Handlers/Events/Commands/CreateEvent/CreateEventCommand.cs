using MediatR;
using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.Handlers.Events.Commands.CreateEvent;

public class CreateEventCommand : IRequest<bool>
{
    public Guid EventId { get; set; }
    public Guid LocationId { get; set; }
    public Guid CategoryId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string UriMainImage { get; set; }
    public DateTime DateTimeEventStart { get; set; }
    public DateTime DateTimeEventEnd { get; set; }
    public bool IsActive { get; set; }
    public ICollection<TicketForExportDto> Tickets { get; set; }

}
