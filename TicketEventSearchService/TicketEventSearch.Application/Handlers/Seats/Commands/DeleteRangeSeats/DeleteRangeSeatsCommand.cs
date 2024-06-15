using MediatR;
using TicketEventSearch.Application.DTOs;

namespace TicketEventSearch.Application.Handlers.Seats.Commands.DeleteRangeSeats;

public class DeleteRangeSeatsCommand : IRequest
{
    public SeatForExportDto[] Seats { get; set; } = default!;
}
