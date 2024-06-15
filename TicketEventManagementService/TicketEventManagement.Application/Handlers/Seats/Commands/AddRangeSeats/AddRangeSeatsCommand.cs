using MediatR;
using TicketEventManagement.Application.DTOs;

namespace TicketEventManagement.Application.Handlers.Seats.Commands.AddRangeSeats;

public class AddRangeSeatsCommand : IRequest<IReadOnlyCollection<GetSeatDto>>
{
    public Guid SchemeId { get; set; }
    public string Sector { get; set; } = default!;
    public int? RowNumberStart { get; set; }
    public int? RowNumberEnd { get; set;}
    public int SeatNumberStart { get; set; }
    public int SeatNumberEnd { get; set; }
}
