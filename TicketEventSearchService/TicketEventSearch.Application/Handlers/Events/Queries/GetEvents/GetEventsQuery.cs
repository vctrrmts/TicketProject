using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Events.Queries.GetEvents;

public class GetEventsQuery : IRequest<IReadOnlyCollection<GetEventDto>>
{
    public Guid? CategoryId { get; set; }
    public Guid? LocationId { get; set; }
    public DateTime? DateEvent { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
}
