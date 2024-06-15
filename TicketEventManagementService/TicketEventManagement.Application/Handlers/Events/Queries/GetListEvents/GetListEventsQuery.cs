using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Events.Queries.GetListEvents;

public class GetListEventsQuery : IRequest<IReadOnlyCollection<GetEventDto>>
{
    public Guid? CategoryId { get; set; }
    public Guid? LocationId { get; set; }
    public DateTime? DateEvent { get; set; }
    public int? Offset { get; set; }
    public int? Limit { get; set; }
    public bool? IsActive { get; set; }
}
