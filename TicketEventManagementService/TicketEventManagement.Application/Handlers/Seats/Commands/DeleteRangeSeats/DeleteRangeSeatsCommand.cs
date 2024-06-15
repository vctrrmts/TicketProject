using MediatR;

namespace TicketEventManagement.Application.Handlers.Seats.Commands.DeleteRangeSeats;

public class DeleteRangeSeatsCommand : IRequest
{
    public Guid SchemeId { get; set; }
    public string Sector { get; set; } = default!;
    public int? RowNumberStart { get; set; }
    public int? RowNumberEnd { get; set; }
    public int? SeatNumberStart { get; set; }
    public int? SeatNumberEnd { get; set; }
}
