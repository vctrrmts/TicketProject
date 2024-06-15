using TicketEventSearch.Domain;

namespace TicketEventSearch.Application.DTOs;

public class GetTicketDto
{
    public Guid TicketId { get; set; }
    public Guid EventId { get; set; }
    public double Price { get; set; }
    public int TicketStatusId { get; set; }
    public DateTime? UnavailableStatusEnd { get; set; }
    public GetSeatDto Seat { get; set; } = default!;
}
